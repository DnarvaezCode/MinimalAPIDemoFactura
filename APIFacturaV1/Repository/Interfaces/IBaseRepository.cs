using APIFacturaV1.Specification;
using APIFacturaV1.Specification.Evaluator;
using System.Linq.Expressions;

namespace APIFacturaV1.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<T> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(T model);
        Task<int> ModificarAsync(T model);
        Task<int> EliminarAsync(int id);
        Task<IEnumerable<T>> ObtenerTodosAsync(ISpecification<T> spesification);
        Task<T> ObtenerEntidadConEspecificacion(ISpecification<T> specification);
    }
}
