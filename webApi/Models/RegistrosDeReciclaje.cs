using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Models;

public class RegistrosDeReciclaje
{
    [Key]
    public int IdRegistrosReciclaje { get; set; }
    [Required]
    [ForeignKey(nameof(IdResidente))]
    public int IdResidente { get; set; }
    [Required]
    [ForeignKey(nameof(IdResiduo))]
    public int IdResiduo { get; set; }
    [Required]
    public decimal PesoKilogramo { get; set; }
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime FechaRegistro { get; set; }
    public int TicketsGanados { get; set; }
    /*===============================================
                //relaciones
    ================================================*/
    public virtual Residuo? Residuo { get; set; }
    public virtual Residente? Residente { get; set; }
}
