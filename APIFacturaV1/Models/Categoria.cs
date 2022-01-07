using APIFacturaV1.Repository.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace APIFacturaV1.Models
{
    public class Categoria : IEntity
    {
        public Categoria()
        {
            Producto = new HashSet<Producto>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la categoria es requerido.")]
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public IEnumerable<Producto> Producto { get; set; }
    }
}
