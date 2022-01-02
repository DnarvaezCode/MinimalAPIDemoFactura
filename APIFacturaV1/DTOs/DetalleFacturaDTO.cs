using System.ComponentModel.DataAnnotations;

namespace APIFacturaV1.DTOs
{
    public class DetalleFacturaDTO
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        [Required(ErrorMessage ="El producto es requerido.")]
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        public float Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
        public virtual ProductoDTO Producto { get; set; }
    }
}
