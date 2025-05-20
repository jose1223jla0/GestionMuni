using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Models;

public class Residente
{
    [Key]
    public int IdResidente { get; set; }
    [Required]
    [ForeignKey(nameof(IdUsuario))]
    public int IdUsuario { get; set; }
    [Required]
    public string? NombreResidente { get; set; }
    [Required]
    public string? ApellidoResidente { get; set; }
    [Required]
    public string? DniResidente { get; set; }
    public string? CorreoResidente { get; set; }
    [Required]
    public string? DireccionResidente { get; set; }
    [Required]
    public bool EstadoResidente { get; set; }
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime FechaCreacionResidente { get; set; }
    [Required]
    public int TicketsTotalesGanados { get; set; }
    /*===============================================
                //relaciones
    ================================================*/
    public Usuario? Usuario { get; set; }
    public List<RegistrosDeReciclaje> RegistrosDeReciclaje { get; set; } = new List<RegistrosDeReciclaje>();
}
