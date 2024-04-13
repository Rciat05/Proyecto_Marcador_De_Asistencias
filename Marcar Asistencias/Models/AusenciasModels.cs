using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marcar_Asistencias.Models
{
    public class AusenciasModels
    {
        [Key]
        public int IDAusencias { get; set; }

        [ForeignKey("EmpleadoID")]
        public int EmpleadoID { get; set; }

        [Required]
        [Display(Name = "TipoAusencia")]
        public string TipoAusencia { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Justificacion")]
        public string Justificacion { get; set; }
    }
}
