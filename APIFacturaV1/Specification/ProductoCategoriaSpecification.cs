using APIFacturaV1.Models;
using APIFacturaV1.Specification.Evaluator;

namespace APIFacturaV1.Specification
{
    public class ProductoCategoriaSpecification : Specification<Producto>
    {
        public ProductoCategoriaSpecification()
        {
            AddInclude(x => x.Categoria);
        }
        public ProductoCategoriaSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(x => x.Categoria);
        }
    }
}
