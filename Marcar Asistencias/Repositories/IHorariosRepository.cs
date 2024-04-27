using Marcar_Asistencias.Models;

namespace Marcar_Asistencias.Repositories
{
    public interface IHorariosRepository
    {
        void Add(HorariosModel horarios);
        void Delete(int id);
        void Edit(HorariosModel horarios);
        IEnumerable<HorariosModel> GetAll();
        HorariosModel? GetById(int id);
    }
}