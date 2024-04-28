using System.ComponentModel.DataAnnotations;

namespace Marcar_Asistencias.Models
{
    public class VacacionesModel
    {
        [Key]
        public int IDVacaciones { get; set; }

        [Display(Name = "Empleado ID")]
        public int? EmpleadoID { get; set; }

        [Display(Name = "Fecha de Inicio")]
        public DateTime? FechaInicio { get; set; }

        [Display(Name = "Fecha de Fin")]
        public DateTime? FechaFin { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

    }
}
