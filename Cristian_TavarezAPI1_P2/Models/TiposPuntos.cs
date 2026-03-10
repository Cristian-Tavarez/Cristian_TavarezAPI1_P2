using System.ComponentModel.DataAnnotations;

namespace Cristian_TavarezAPI1_P2.Models;

public class TiposPuntos
{
    [Key]
    public int TipoId { get; set; }
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este Campo Es Requerido")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este Campo Es Requerido")]
    public int ValorPuntos { get; set; }

    [Required(ErrorMessage = "Este Campo Es Requerido")]
    public string Color { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este Campo Es Requerido")]
    public string Icono { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;
}