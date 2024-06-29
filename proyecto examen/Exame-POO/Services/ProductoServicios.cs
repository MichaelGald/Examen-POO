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
                NombrePro = dto.NombrePro,
                Precio = dto.Precio,
            };

            productsDtos.Add(productDto);

            var productos = productsDtos.Select(x => new ProductoEntity
            {
               IdProducto = x.IdProducto,
               NombrePro = x.NombrePro,
               Precio = x.Precio,
            }).ToList();

            await WriteProductsToFileAsync(productos);
            return true;
        }

        public async Task<bool> DeleteProdctoAsync(Guid id)
        {
            var productsDto = await ReadProductsFromFileAsync();
            var productsToDelete = productsDto.FirstOrDefault(x => x.IdProducto == id);
            if (productsToDelete is null)
            {
                return false;
            }
            productsDto.Remove(productsToDelete);
            var products = productsDto.Select(x => new ProductoEntity
            {
               IdProducto= x.IdProducto,
               NombrePro= x.NombrePro,
               Precio= x.Precio,
            }).ToList();
            await WriteProductsToFileAsync(products);
            return true;
        }

        public async Task<bool> EditProductoAsync(ProductoEditDto dto, Guid id)
        {
            var productosDto = await ReadProductsFromFileAsync();
            var existingProductos = productosDto.FirstOrDefault(producto => producto.IdProducto == id);
            if (existingProductos is null)
            {
                return false;
            }

            //TODO: Recorrer las cetgorias y actualizar la correspodiente de la lista
            for (int i = 0; i < productosDto.Count; i++)
            {
                if (productosDto[i].IdProducto == id)
                {
                    productosDto[i].NombrePro = dto.NombrePro;
                    productosDto[i].Precio = dto.Precio;
                }
            }

            var producto = productosDto.Select(x => new ProductoEntity
            {
                IdProducto= x.IdProducto,
                NombrePro= x.NombrePro, 
                Precio= x.Precio,
            }).ToList();

            await WriteProductsToFileAsync(producto);
            return true;
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
