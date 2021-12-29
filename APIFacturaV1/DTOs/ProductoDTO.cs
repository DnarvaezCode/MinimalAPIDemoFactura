using APIFacturaV1.Models;
using System.ComponentModel.DataAnnotations;

namespace APIFacturaV1.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public double Stock { get; set; }
        public byte[] Imagen { get; set; }
        public bool Estado { get; set; }
        public virtual CategoriaDTO Categoria { get; set; }
    }
}
