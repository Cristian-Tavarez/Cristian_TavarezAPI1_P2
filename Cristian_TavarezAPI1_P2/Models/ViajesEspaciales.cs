using System.ComponentModel.DataAnnotations;

namespace Cristian_TavarezAPI1_P2.Models;
public class ViajesEspaciales
{
    [Key]
    public int ViajeId { get; set; }

    [Required(ErrorMessage = "La fecha es requerida")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "El costo es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor que cero")]
    public decimal Costo { get; set; }

    [Required(ErrorMessage = "La descripcion es requerida")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "La descripcion debe tener entre 5 a 100 caracteres")]
    public string Descripcion { get; set; } = string.Empty;
}