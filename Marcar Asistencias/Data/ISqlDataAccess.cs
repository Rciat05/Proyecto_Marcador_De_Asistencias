using System.Data;

namespace Marcar_Asistencias.Data
{
    public interface ISqlDataAccess
    {
        IDbConnection GetConnection();
    }
}