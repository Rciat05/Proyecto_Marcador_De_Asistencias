using System.ComponentModel.DataAnnotations;

namespace Marcar_Asistencias.Models
{
    public class ComentariosModel
    {
        [Key]
        public int ComentarioNotaID { get; set; }

        [Required]
        [Display(Name = "UsuarioID")]
        public int UsuarioID { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Required]
        [Display(Name = "Fecha de comentario")]
        public DateTime FechaHora { get; set; }

        [Required]
        [Display(Name = "Texto")]
        public string Texto { get; set; }
    }
}
