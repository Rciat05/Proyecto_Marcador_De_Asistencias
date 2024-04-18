using proyectometodologias.models;

namespace Marcar_Asistencias.Repositories
{
    public interface IEmpleadosRepository
    {
        void Add(EmpleadosModel empleados);
        void Delete(int id);
        void Edit(EmpleadosModel empleados);
        IEnumerable<EmpleadosModel> GetAll();
        EmpleadosModel? GetById(int id);
    }
}