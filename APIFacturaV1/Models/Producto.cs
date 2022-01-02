using APIFacturaV1.Repository.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace APIFacturaV1.Models
{
    public partial class Producto : IEntity
    {
        public Producto()
        {
            DetalleFactura = new HashSet<DetalleFactura>();
        }
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public double Stock { get; set; }
        public byte[] Imagen { get; set; }
        public bool Estado { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual IEnumerable<DetalleFactura> DetalleFactura { get; set; }
    }
}
