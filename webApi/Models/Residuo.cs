using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Models
{
    public class Residuo
    {
        [Key]
        public int IdResiduo { get; set; }
        [Required]
        [ForeignKey(nameof(IdCategoriaResiduo))]
        public int IdCategoriaResiduo { get; set; }
        [Required]
        public string? NombreResiduo { get; set; }

        [Required]
        public bool EstadoResiduo { get; set; }
        /*===============================================
                //relaciones
        ================================================*/
        public CategoriaResiduo? CategoriaResiduo { get; set; }
        public List<RegistrosDeReciclaje> RegistrosDeReciclaje { get; set; } = new List<RegistrosDeReciclaje>();
    }
}