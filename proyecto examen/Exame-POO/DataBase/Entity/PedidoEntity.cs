using System.ComponentModel.DataAnnotations;

namespace Exame_POO.DataBase.Entity
{
    public class PedidoEntity  : BaseEntity
    {
        public Guid IdPedido { get; set; }
        [Display(Name = "Tiempo")]
        public DateTime Tiempo { get; set; }
        [Display(Name = "ToTal del pedido")]
        public decimal Total { get; set; }
    }
}
