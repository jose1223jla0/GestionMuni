using System.ComponentModel.DataAnnotations;
namespace webApi.Models;

public class Premio
{
    [Key]
    public int IdPremio { get; set; }
    [Required]
    public string? NombrePremio { get; set; }
    [Required]
    public string? DescripcionPremio { get; set; }
    [Required]
    public int PuntosRequeridos { get; set; }
    [Required]
    public bool EstadoPremio { get; set; }
    /*===============================================
                //relaciones
    ================================================*/
    public List<Canje> Canjes { get; set; } = new List<Canje>();
}
