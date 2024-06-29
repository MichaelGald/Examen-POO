namespace Exame_POO.Dtos.Gestion.Pedidos
{
    public class PedidoCreateDto
    {
        public List<Guid> ListaProductos { get; set; }

        public decimal Total { get; set; }
        
    }
}
