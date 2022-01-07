using System.ComponentModel.DataAnnotations;

namespace APIFacturaV1.Models
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        [Required(ErrorMessage = "El producto es requerido.")]
        public int ProductoId { get; set; }
        [Required(ErrorMessage = "La descripcion es requerido.")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La cantidad es requerido.")]
        public float Cantidad { get; set; }
        [Required(ErrorMessage = "El precio es requerido.")]
        public decimal PrecioUnidad { get; set; }
        public decimal SubTotal { get; set; }
        public Factura Factura { get; set; }
        public Producto Producto { get; set; }
    }
}
