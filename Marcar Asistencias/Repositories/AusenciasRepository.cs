using Dapper;
using Marcar_Asistencias.Data;
using Marcar_Asistencias.Models;
using proyectometodologias.models;
using System.Data;

namespace Marcar_Asistencias.Repositories
{
    public class AusenciasRepository : IAusenciasRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public IEnumerable<AusenciasModels> GetAll()
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spAusencias_GetAll";

                return connection.Query<AusenciasModels>(
                                  storeProcedure,
                                  commandType: CommandType.StoredProcedure
                                    );
            }
        }

        public void Add(AusenciasModels ausencias)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spAusencias_Insert";

                connection.Execute
                    (
                        storeProcedure,
                        new { ausencias.EmpleadoID, ausencias.Fecha, ausencias.TipoAusencia, ausencias.Justificacion},
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Delete(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spAusencias_Delete";

                connection.Execute
                    (
                        storeProcedure,
                        new { IDAusencias = id },
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public void Edit(AusenciasModels ausencias)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spAusencias_Update";

                connection.Execute
                    (
                        storeProcedure,
                        ausencias,
                        commandType: CommandType.StoredProcedure
                    );
            }
        }

        public AusenciasModels? GetById(int id)
        {
            using (var connection = _dataAccess.GetConnection())
            {
                string storeProcedure = "dbo.spAusencias_GetById";

                return connection.QueryFirstOrDefault<AusenciasModels>(
                                  storeProcedure,
                                  new { IDAusencias = id },
                                  commandType: CommandType.StoredProcedure
                                    );
            }
        }
    }
}
