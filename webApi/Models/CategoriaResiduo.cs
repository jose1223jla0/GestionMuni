using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Models
{
    public class CategoriaResiduo
    {
        [Key]
        public int IdCategoriaResiduo { get; set; }
        [Required]
        [ForeignKey(nameof(IdTicket))]
        public int IdTicket { get; set; }
        public string? NombreCategoria { get; set; }
        [Required]
        public bool EstadoCategoriaResiduo { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime FechaCreacionCategoriaResiduo { get; set; }
        /*===============================================
                //relaciones
        ================================================*/
        public Ticket? Ticket { get; set; }
        public List<Residuo> Residuos { get; set; } = new List<Residuo>();

    }
}