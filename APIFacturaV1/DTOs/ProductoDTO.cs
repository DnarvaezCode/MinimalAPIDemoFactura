using APIFacturaV1.Models;
using System.ComponentModel.DataAnnotations;

namespace APIFacturaV1.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La categoria es requerida.")]
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripción es requerida.")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El precio es requerido.")]
        public decimal Precio { get; set; }
        public byte[] Imagen { get; set; }
        public bool Estado { get; set; }
        public virtual CategoriaDTO Categoria { get; set; }
    }
}
