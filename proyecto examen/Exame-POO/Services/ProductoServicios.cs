using Exame_POO.DataBase.Entity;
using Exame_POO.Dtos.Gestion.Productos;
using Exame_POO.Services.Interfaces;
using Newtonsoft.Json;

namespace Exame_POO.Services
{
    public class ProductoServicios : IProductoServicios
    {
        public readonly string _JSON_FILE;
        public ProductoServicios()
        {
            _JSON_FILE = "SeedData/Producto.json";
        }

        public async Task<bool> CreateProductoAsync(ProductoCreateDto dto)
        {
            var productsDtos = await ReadProductsFromFileAsync();

            if (productsDtos.Any(p => p.NombrePro.Equals(dto.NombrePro, StringComparison.OrdinalIgnoreCase)))
            {
                return false; // O lanzar una excepción dependiendo de tu manejo de errores
            }

            var productDto = new ProductoDto
            {
                IdProducto = Guid.NewGuid(),
               

            };
            productsDtos.Add(productDto);

            var productos = productsDtos.Select(x => new ProductEntity
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,

            }).ToList();

            await WriteProductsToFileAsync(productos);
            return true;
        }

        public Task<bool> DeleteProdctoAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditProductoAsync(ProductoEditDto dto, Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductoDto> GetProductoByIdAsync(Guid id)
        {
            var products = await ReadProductsFromFileAsync();
            ProductoDto product = products.FirstOrDefault(c => c.IdProducto == id);
            return product;
        }

        public async Task<List<ProductoDto>> GetProductosListAsync()
        {
            return await ReadProductsFromFileAsync();
        }

        private async Task<List<ProductoDto>> ReadProductsFromFileAsync()
        {
            if (!File.Exists(_JSON_FILE))
            {
                return new List<ProductoDto>();
            }
            var json = await File.ReadAllTextAsync(_JSON_FILE);
            var categories = JsonConvert.DeserializeObject<List<ProductoEntity>>(json);
            var dtos = categories.Select(x => new ProductoDto
            {
                IdProducto = x.IdProducto,
                NombrePro = x.NombrePro,
                Precio = x.Precio,
            }).ToList();

            return dtos;
        }
        private async Task WriteProductsToFileAsync(List<ProductoEntity> productos)
        {
            var json = JsonConvert.SerializeObject(productos, Formatting.Indented);

            if (File.Exists(_JSON_FILE))
            {
                await File.WriteAllTextAsync(_JSON_FILE, json);
            }
        }

    }
}
