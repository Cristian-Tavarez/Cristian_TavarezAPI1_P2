using Cristian_TavarezAPI1_P2.Models;
using Microsoft.EntityFrameworkCore;

namespace Cristian_TavarezAPI1_P2.Context;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Estudiantes> Estudiantes { get; set; }
    public DbSet<TiposPuntos> TiposPuntos { get; set; }
    public DbSet<AsignacionesPuntos> AsignacionesPuntos { get; set; }
    public DbSet<AsignacionesPuntosDetalle> AsignacionesPuntosDetalle { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<Estudiantes>().HasData(
            new Estudiantes { EstudianteId = 1, Nombres = "Ana Martínez", Email = "ana@universidad.edu", Edad = 20, BalancePuntos = 0 },
            new Estudiantes { EstudianteId = 2, Nombres = "Carlos Pérez", Email = "carlos@universidad.edu", Edad = 22, BalancePuntos = 0 },
            new Estudiantes { EstudianteId = 3, Nombres = "Laura Rodríguez", Email = "laura@universidad.edu", Edad = 21, BalancePuntos = 0 }
        );

        
        modelBuilder.Entity<TiposPuntos>().HasData(
            new TiposPuntos { TipoId = 1, Nombre = "Participación", Descripcion = "Participación en clase", ValorPuntos = 5, Color = "primary", Icono = "bi-hand-thumbs-up", Activo = true },
            new TiposPuntos { TipoId = 2, Nombre = "Tarea Entregada", Descripcion = "Entrega de tarea", ValorPuntos = 10, Color = "success", Icono = "bi-journal-check", Activo = true },
            new TiposPuntos { TipoId = 3, Nombre = "Proyecto", Descripcion = "Entrega de proyecto", ValorPuntos = 20, Color = "warning", Icono = "bi-lightbulb", Activo = true }
        );
    }
}