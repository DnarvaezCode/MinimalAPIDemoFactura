using APIFacturaV1.Context;
using APIFacturaV1.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APIFacturaV1.Repository
{
    public class StoreProcedureRepository<T> : IStoreProcedureRepository<T> where T : class
    {
        private readonly APIFacturaContext _context;

        public StoreProcedureRepository(APIFacturaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetDataAsync(string storeProcedureName, params object[] parameters)
        {
            return await _context.Set<T>().FromSqlRaw<T>(storeProcedureName, parameters).ToListAsync();
        }

        public async Task<int> SaveDataAsync(string storeProcedureName, SqlParameter sqlParameter, params object[] parameters)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlRawAsync(storeProcedureName, parameters);
                if (sqlParameter is not null) return Convert.ToInt32(sqlParameter.Value);
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
