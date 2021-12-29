using APIFacturaV1.Context;
using APIFacturaV1.Repository.Interfaces;
using APIFacturaV1.Specification;
using APIFacturaV1.Specification.Evaluator;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APIFacturaV1.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        private readonly APIFacturaContext _context;

        public BaseRepository(APIFacturaContext context)
        {
            _context = context;
        }
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

        public async Task<IEnumerable<T>> ObtenerTodosAsync(ISpecification<T> spesification)
        {
            return await AplicarEspecificacion(spesification).ToListAsync();
        }

        private IQueryable<T> AplicarEspecificacion(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
        }

        public async Task<T> ObtenerEntidadConEspecificacion(ISpecification<T> specification)
        {
            return await AplicarEspecificacion(specification).FirstOrDefaultAsync();
        }

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

        public async Task<T> ObtenerPorIdAsync(int id)
        {
            return await _context.Set<T>()
               .AsNoTracking()
               .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<T>> ObtenerTodosAsync()
        {

            return await _context.Set<T>()
               .AsNoTracking()
               .Where(m => m.Estado == true)
               .ToListAsync();
        }
    }
}
