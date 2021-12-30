using System.ComponentModel.DataAnnotations;

namespace APIFacturaV1.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }
}
