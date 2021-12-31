using Microsoft.Data.SqlClient;

namespace APIFacturaV1.Repository.Interfaces
{
    public interface IStoreProcedureRepository<T> where T : class
    {
        Task<int> SaveDataAsync(string nameStoreProcedure, SqlParameter sqlParameter, params object[] parameters);
        Task<IEnumerable<T>> GetDataAsync(string nameStoreProcedure, params object[] parameters);
    }
}
