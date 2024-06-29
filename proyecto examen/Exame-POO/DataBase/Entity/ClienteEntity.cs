using System.ComponentModel.DataAnnotations;

namespace Exame_POO.DataBase.Entity
{
    public class ClienteEntity : BaseEntity
    {
        [Display(Name = "Nombre de Clinete")]
        [Required(ErrorMessage = "El {0} de la categoria es requerida.")]
        public string NombreCli { get; set; }
        [Display(Name = "Email")]
        [MinLength(25, ErrorMessage = "El {0} debe tener al menos {1} caracter.")]
        public string Email { get; set; }
    }
}
