using Marcar_Asistencias.Models;

namespace Marcar_Asistencias.Repositories
{
    public interface IVacacionesRepository
    {
        void Add(VacacionesModel vacaciones);
        void Delete(int id);
        void Edit(VacacionesModel vacaciones);
        IEnumerable<VacacionesModel> GetAll();
        VacacionesModel? GetById(int id);
    }
}