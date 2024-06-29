using Exame_POO.DataBase.Entity;
using Exame_POO.Dtos.Gestion.Pedidos;
using Exame_POO.Dtos.Gestion.Productos;
using Exame_POO.Services.Interfaces;
using Newtonsoft.Json;

namespace Exame_POO.Services
{
    public class PedidoServicios : IPedidoServicios
    {
        public readonly string _PEDIDOS_JSON_FILE;
        public readonly string _PRODUCTOS_JSON_FILE;
        private List<ProductoDto> _productos;

        public PedidoServicios()
        {
            _PEDIDOS_JSON_FILE = "SeedData/Pedido.json";
            _PRODUCTOS_JSON_FILE = "SeedData/Producto.json";
            _productos = new List<ProductoDto>();
            LoadProductos().Wait();
        }

        private async Task LoadProductos()
        {
            if (File.Exists(_PRODUCTOS_JSON_FILE))
            {
                var json = await File.ReadAllTextAsync(_PRODUCTOS_JSON_FILE);
                _productos = JsonConvert.DeserializeObject<List<ProductoDto>>(json);
            }
        }

        public async Task<bool> CreatePedidoAsync(PedidoCreateDto dto)
        {
            var pedidosDtos = await ReadPedidosFromFileAsync();

            var pedidoDto = new PedidosDto
            {
                IdPedido = Guid.NewGuid(),
                IdCliente = Guid.NewGuid(),
                ListaProductos = dto.ListaProductos,
                Tiempo = DateTime.Now,
                Total = CalcularTotal(dto.ListaProductos),
            };
            pedidosDtos.Add(pedidoDto);

            var productos = pedidosDtos.Select(x => new PedidoEntity
            {
               IdPedido = x.IdPedido,
               IdCliente = x.IdCliente,
               Tiempo = x.Tiempo,
               Total = x.Total,

            }).ToList();

            await WritePedidosToFileAsync(productos);
            return true;
        }

        public async Task<bool> DeletePedidoAsync(Guid id)
        {
            var pedidosDto = await ReadPedidosFromFileAsync();
            var pedidosToDelete = pedidosDto.FirstOrDefault(x => x.IdPedido == id);
            if (pedidosToDelete is null)
            {
                return false;
            }
            pedidosDto.Remove(pedidosToDelete);
            var pedidos = pedidosDto.Select(x => new PedidoEntity
            {
                IdPedido = x.IdPedido,
                IdCliente = x.IdCliente, 
                Tiempo = x.Tiempo, 
                Total = x.Total,
            }).ToList();
            await WritePedidosToFileAsync(pedidos);
            return true;
        }

        public async Task<bool> EditPedidoAsync(PedidoEditDto dto, Guid id)
        {
            var pedidosDto = await ReadPedidosFromFileAsync();
            var existingProduct = pedidosDto.FirstOrDefault(producto => producto.IdPedido == id);
            if (existingProduct is null)
            {
                return false;
            }

            for (int i = 0; i < pedidosDto.Count; i++)
            {
                if (pedidosDto[i].IdPedido == id)
                {
                    pedidosDto[i].ListaProductos = dto.ListaProductos;
                }
            }

            var pedidos = pedidosDto.Select(x => new PedidoEntity
            {
                IdPedido = x.IdPedido,
                IdCliente = x.IdCliente,
                Tiempo = x.Tiempo,
                ListaPedido = x.ListaProductos,
                Total = x.Total,
            }).ToList();

            await WritePedidosToFileAsync(pedidos);
            return true;
        }

        public async Task<PedidosDto> GetPedidoByIdAsync(Guid id)
        {
            var pedidos = await ReadPedidosFromFileAsync();
            PedidosDto product = pedidos.FirstOrDefault(c => c.IdPedido == id);
            return product;
        }

        public async Task<List<PedidosDto>> GetPedidosListAsync()
        {
            return await ReadPedidosFromFileAsync();
        }

        private async Task<List<PedidosDto>> ReadPedidosFromFileAsync()
        {
            if (!File.Exists(_PEDIDOS_JSON_FILE))
            {
                return new List<PedidosDto>();
            }
            var json = await File.ReadAllTextAsync(_PEDIDOS_JSON_FILE);
            var categories = JsonConvert.DeserializeObject<List<PedidoEntity>>(json);
            var dtos = categories.Select(x => new PedidosDto
            {
                IdPedido = x.IdPedido,
                IdCliente = x.IdCliente,
                Tiempo = x.Tiempo,
                ListaProductos = x.ListaPedido,
                Total = x.Total,
            }).ToList();

            return dtos;
        }
        private async Task WritePedidosToFileAsync(List<PedidoEntity> pedidos)
        {
            var json = JsonConvert.SerializeObject(pedidos, Formatting.Indented);

            if (File.Exists(_PEDIDOS_JSON_FILE))
            {
                await File.WriteAllTextAsync(_PEDIDOS_JSON_FILE, json);
            }
        }
        private decimal CalcularTotal(List<Guid> listaProductos)
        {
            return listaProductos.Sum(productoId => _productos.FirstOrDefault(p => p.IdProducto == productoId)?.Precio ?? 0);
        }
    }
}
