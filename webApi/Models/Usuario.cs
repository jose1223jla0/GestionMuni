using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace webApi.Models;

public class Usuario
{
    [Key]
    public int IdUsuario { get; set; }
    [Required]
    public Rol IdRol { get; set; }
    public string? NombreUsuario { get; set; }
    [Required]
    public string? Contrasena { get; set; }
    [Required]
    public bool EstadoUsuario { get; set; }=true;
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime FechaCreacionUsuario { get; set; }
    /*===============================================
                //relaciones
    ================================================*/
    public List<Residente> Residentes { get; set; } = new List<Residente>();
}
