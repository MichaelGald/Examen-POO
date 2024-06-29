using Exame_POO.Dtos.Gestion.Clientes;
using Exame_POO.Dtos.Gestion.Pedidos;

namespace Exame_POO.Services.Interfaces
{
    public interface IClienteServicios
    {
        Task<List<ClientesDto>> GetClientesListAsync();
        Task<ClientesDto> GetClienteByIdAsync(Guid id);
        Task<bool> CreateClienteAsync(ClientesCreateDto dto);
        Task<bool> EditClienteAsync(ClienteEditDto dto, Guid id);
        Task<bool> DeleteClienteAsync(Guid id);
    }
}
