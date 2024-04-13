using Dapper;
using Marcar_Asistencias.Data;
using proyectometodologias.models;
using System.Data;

namespace Marcar_Asistencias.Repositories
{
    public class EmpleadosRepository : IEmpleadosRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public EmpleadosRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<EmpleadosModel> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "Empleados_GetAll";

                return connection.Query<EmpleadosModel>(
                                  storeProcedure,
                                  commandType: CommandType.StoredProcedure
                                    );
            }
        }

        public EmpleadosModel? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spEmpleados_GetById";

                return connection.QueryFirstOrDefault<EmpleadosModel>(
                                  storeProcedure,
                                  new { EmpleadoID = id },
                                  commandType: CommandType.StoredProcedure
                                    );
            }
        }

        public void Add(EmpleadosModel empleados)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spEmpleados_Insert";

                connection.Execute
                    (
                        storeProcedure,
                        new { empleados.Nombre, empleados.Apellido, empleados.FechaNacimiento, empleados.Departamento },
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
              
                    string storeProcedure = "dbo.spEmpleados_Delete";

                connection.Execute(
                     storeProcedure,
                     new { EmpleadosID = id },
                     commandType: CommandType.StoredProcedure
                 );
            }
        }

        public void Edit(EmpleadosModel empleados)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spEmpleados_Update";

                connection.Execute(
                        storeProcedure,
                        empleados,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }
    }
}
