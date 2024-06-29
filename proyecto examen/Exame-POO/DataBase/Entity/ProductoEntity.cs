using System.ComponentModel.DataAnnotations;

namespace Exame_POO.DataBase.Entity
{
    public class ProductoEntity
    {
        public Guid IdProducto { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} de la categoria es requerida.")]
        public string NombrePro { get; set; }
        [Display(Name = "Precio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El {0} no puede ser $0.")]
        public decimal Precio { get; set; }
    }
}
