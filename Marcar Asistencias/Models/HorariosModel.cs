using System.ComponentModel.DataAnnotations;

namespace Marcar_Asistencias.Models
{
	public class HorariosModel
	{

		[Key]
		public int HorarioID { get; set; }

		[Required]
		[Display(Name = "Nombre Horario")]
		public string NombreHorario { get; set; }

		[Required]
		[Display(Name = "DiasLaborables")]
		public string DiasLaborables { get; set; }

	}
}
