using Microsoft.EntityFrameworkCore;
using Cristian_TavarezAPI1_P2.Context;
using Cristian_TavarezAPI1_P2.Models;

namespace Cristian_TavarezAPI1_P2.Services;
public class AsignacionesService
{
    private readonly Contexto _context;
    public AsignacionesService(Contexto context) => _context = context;
    public async Task<List<Estudiantes>> ListarEstudiantes() => await _context.Estudiantes.ToListAsync();

    public async Task CrearEstudianteRapido(Estudiantes estudiante)
    {
        _context.Estudiantes.Add(estudiante);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TiposPuntos>> ListarTiposActivos() =>
        await _context.TiposPuntos.Where(t => t.Activo).ToListAsync();

    public async Task GuardarTipoPunto(TiposPuntos tipo)
    {
        if (tipo.TipoId == 0)
            _context.TiposPuntos.Add(tipo);
        else
            _context.Update(tipo);

        await _context.SaveChangesAsync();
    }

    public async Task EliminarTipoPunto(int id)
    {
        var tipo = await _context.TiposPuntos.FindAsync(id);
        if (tipo != null)
        {
           
            _context.TiposPuntos.Remove(tipo);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<AsignacionesPuntos?> BuscarPorId(int id) =>
        await _context.AsignacionesPuntos
            .Include(a => a.Detalles)
            .ThenInclude(d => d.TipoPunto)
            .FirstOrDefaultAsync(a => a.IdAsignacion == id);

    public async Task Guardar(AsignacionesPuntos asignacion)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            asignacion.TotalPuntos = asignacion.Detalles.Sum(d => d.CantidadPuntos);

            if (asignacion.IdAsignacion != 0)
            {
                var anterior = await _context.AsignacionesPuntos.AsNoTracking()
                    .FirstOrDefaultAsync(a => a.IdAsignacion == asignacion.IdAsignacion);

                if (anterior != null)
                {
                    await ModificarBalance(anterior.EstudianteId, -anterior.TotalPuntos);
                }

                await _context.Database.ExecuteSqlRawAsync("DELETE FROM AsignacionesPuntosDetalle WHERE IdAsignacion = {0}", asignacion.IdAsignacion);
                _context.Update(asignacion);
            }
            else
            {
                _context.AsignacionesPuntos.Add(asignacion);
            }

            await ModificarBalance(asignacion.EstudianteId, asignacion.TotalPuntos);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task ModificarBalance(int estudianteId, int puntos)
    {
        var est = await _context.Estudiantes.FindAsync(estudianteId);
        if (est != null)
        {
            est.BalancePuntos += puntos;
            _context.Entry(est).State = EntityState.Modified;
        }
    }

    public async Task Eliminar(int id)
    {
        var asig = await BuscarPorId(id);
        if (asig != null)
        {
            await ModificarBalance(asig.EstudianteId, -asig.TotalPuntos);
            _context.AsignacionesPuntos.Remove(asig);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<AsignacionesPuntos>> ListarConFiltros(int? estId, DateTime? desde, DateTime? hasta)
    {
        var query = _context.AsignacionesPuntos
            .Include(a => a.Estudiante)
            .Include(a => a.Detalles)
                .ThenInclude(d => d.TipoPunto)
            .AsQueryable();

        if (estId.HasValue && estId > 0) query = query.Where(a => a.EstudianteId == estId);
        if (desde.HasValue) query = query.Where(a => a.Fecha.Date >= desde.Value.Date);
        if (hasta.HasValue) query = query.Where(a => a.Fecha.Date <= hasta.Value.Date);

        return await query.ToListAsync();
    }
}