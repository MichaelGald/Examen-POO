namespace Exame_POO.Dtos.Gestion.Pedidos
{
    public class PedidosDto
    {
        public Guid IdPedido { get; set; }

        public Guid IdCliente { get; set; }

        public DateTime Tiempo { get; set; }

        public List<Guid> ListaProductos { get; set; }

        public decimal Total { get; set; }
    }
}
