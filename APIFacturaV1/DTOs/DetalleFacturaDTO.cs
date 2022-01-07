using System.ComponentModel.DataAnnotations;

namespace APIFacturaV1.DTOs
{
    public class DetalleFacturaDTO
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
        public virtual ProductoDTO Producto { get; set; }
    }
}
