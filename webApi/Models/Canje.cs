using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Models;

public class Canje
{
    [Key]
    public int IdCanje { get; set; }
    [Required]
    [ForeignKey(nameof(IdResidente))]
    public int IdResidente { get; set; }
    [Required]
    [ForeignKey(nameof(IdPremio))]
    public int IdPremio { get; set; }
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime FechaDeCanjeo { get; set; }
    [Required]
    public bool EstadoCanje { get; set; }
    /*===============================================
             //relaciones
    ================================================*/
    public Residente? Residente { get; set; }
    public Premio? Premio { get; set; }
}
