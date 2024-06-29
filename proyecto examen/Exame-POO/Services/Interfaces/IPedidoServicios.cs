using Exame_POO.Dtos.Gestion.Pedidos;

namespace Exame_POO.Services.Interfaces
{
    public interface IPedidoServicios
    {
        Task<List<PedidosDto>> GetPedidosListAsync();
        Task<PedidosDto> GetPedidoByIdAsync(Guid id);
        Task<bool> CreatePedidoAsync(PedidoCreateDto dto);
        Task<bool> EditPedidoAsync(PedidoEditDto dto, Guid id);
        Task<bool> DeletePedidoAsync(Guid id);
    }
}
