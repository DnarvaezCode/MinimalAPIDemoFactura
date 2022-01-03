using APIFacturaV1.Context;
using APIFacturaV1.Repository.Interfaces;
using APIFacturaV1.Specification;
using APIFacturaV1.Specification.Evaluator;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APIFacturaV1.Repository
{
    public class GeneryRepository<T> : IGeneryRepository<T> where T : class, IEntity
    {
        private readonly APIFacturaContext _context;

        public GeneryRepository(APIFacturaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método genérico para eliminar data.
        /// </summary>
        /// <param name="id">Id por el que se desea eliminar.</param>
        /// <returns>Retorna el id del registro eliminado.</returns>
        public async Task<int> EliminarAsync(int id)
        {
            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    var model = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
                    _context.Remove(model);
                    await _context.SaveChangesAsync();

                    transaccion.Commit();
                    return model.Id;

                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// Método para obtener toda la data.
        /// </summary>
        /// <param name="spesification">Especificación para incluir entidades relacionadas.</param>
        /// <returns>Retorna una lista de tipo T.</returns>
        public async Task<IEnumerable<T>> ObtenerTodosAsync(ISpecification<T> spesification)
        {
            return await AplicarEspecificacion(spesification).ToListAsync();
        }

        /// <summary>
        /// Método para aplicar la especificaciones.
        /// </summary>
        /// <param name="specification">Parámetro de especificación para incluir entidades relacionadas.</param>
        /// <returns>Retorna un IQueryable de tipo T.</returns>
        private IQueryable<T> AplicarEspecificacion(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
        }

        /// <summary>
        /// Método para obtener entidad con especificación.
        /// </summary>
        /// <param name="specification">Parámetro con el criterio de especificación.</param>
        /// <returns></returns>
        public async Task<T> ObtenerEntidadConEspecificacion(ISpecification<T> specification)
        {
            return await AplicarEspecificacion(specification).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Método genérico para insertar data.
        /// </summary>
        /// <param name="model">Modelo a insertar</param>
        /// <returns>Retorna un entero con el id del nuevo registro</returns>
        public async Task<int> InsertarAsync(T model)
        {
            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<T>().Add(model);
                    await _context.SaveChangesAsync();

                    transaccion.Commit();
                    return model.Id;

                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// Método genérico para actualizar data.
        /// </summary>
        /// <param name="model">Modelo a actualizar.</param>
        /// <returns>Retorna un entero con el id del registro actualizado.</returns>
        public async Task<int> ModificarAsync(T model)
        {
            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<T>().Update(model);
                    await _context.SaveChangesAsync();

                    transaccion.Commit();
                    return model.Id;

                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// Método genérico para obtener data por id.
        /// </summary>
        /// <param name="id">Id a filtrar</param>
        /// <returns>Retorna una entidad de tipo T.</returns>
        public async Task<T> ObtenerPorIdAsync(int id)
        {
            return await _context.Set<T>()
               .AsNoTracking()
               .FirstOrDefaultAsync(m => m.Id == id);
        }

        /// <summary>
        /// Método genérico para obtener data.
        /// </summary>
        /// <returns>Retorna una lista de tipo T.</returns>
        public async Task<IEnumerable<T>> ObtenerTodosAsync()
        {

            return await _context.Set<T>()
               .AsNoTracking()
               .Where(m => m.Estado == true)
               .ToListAsync();
        }
    }
}
