using APIFacturaV1.Specification;
using System.Linq.Expressions;

namespace APIFacturaV1.Repository.Interfaces
{
    public interface IGeneryRepository<T> where T : class
    {
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<T> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(T model);
        Task<int> ModificarAsync(T model);
        Task<int> EliminarAsync(int id);
        Task<IEnumerable<T>> GetAllWithSpecificationAsync(ISpecification<T> spesification);
        Task<T> GetEntityWithSpecificationAsync(ISpecification<T> specification);
    }
}
