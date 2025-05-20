
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Models
{
    public class Ticket
    {
        [Key]
        public int IdTicket { get; set; }
        [Required]
        public string? ColorTicket { get; set; }
        [Required]
        public bool EstadoTicket { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime FechaCreacionTicket { get; set; }
        /*===============================================
                //relaciones
        ================================================*/
        public List<CategoriaResiduo> CategoriaResiduos { get; set; } = new List<CategoriaResiduo>();
    }
}