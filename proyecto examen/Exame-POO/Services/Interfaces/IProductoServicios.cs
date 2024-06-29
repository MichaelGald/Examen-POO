using Exame_POO.Dtos.Gestion.Pedidos;
using Exame_POO.Dtos.Gestion.Productos;

namespace Exame_POO.Services.Interfaces
{
    public interface IProductoServicios
    {
        Task<List<ProductoDto>> GetProductosListAsync();
        Task<ProductoDto> GetProductoByIdAsync(Guid id);
        Task<bool> CreateProductoAsync(ProductoCreateDto dto);
        Task<bool> EditProductoAsync(ProductoEditDto dto, Guid id);
        Task<bool> DeleteProdctoAsync(Guid id);
    }
}
