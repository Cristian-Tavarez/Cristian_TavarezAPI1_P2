using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cristian_TavarezAPI1_P2.Models;
public class AsignacionesPuntos
{
    [Key]
    public int IdAsignacion { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estudiante")]
    public int EstudianteId { get; set; }

    [ForeignKey("EstudianteId")]
    public Estudiantes? Estudiante { get; set; }

    public int TotalPuntos { get; set; }

    public List<AsignacionesPuntosDetalle> Detalles { get; set; } = new();
}

public class AsignacionesPuntosDetalle
{
    [Key]
    public int IdDetalle { get; set; }
    public int IdAsignacion { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Seleccione un tipo de punto")]
    public int TipoPuntoId { get; set; }

    [ForeignKey("TipoPuntoId")]
    public TiposPuntos? TipoPunto { get; set; }

    public int CantidadPuntos { get; set; }
}