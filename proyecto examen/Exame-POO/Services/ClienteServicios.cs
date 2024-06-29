using Exame_POO.DataBase.Entity;
using Exame_POO.Dtos.Gestion.Clientes;
using Exame_POO.Dtos.Gestion.Productos;
using Exame_POO.Services.Interfaces;
using Newtonsoft.Json;

namespace Exame_POO.Services
{
    public class ClienteServicios : IClienteServicios
    {
        public readonly string _JSON_FILE;
        public ClienteServicios()
        {
            _JSON_FILE = "SeedData/Cliente.json";
        }

        public async Task<bool> CreateClienteAsync(ClientesCreateDto dto)
        {
            var productsDtos = await ReadClientesFromFileAsync();

            if (productsDtos.Any(p => p.NombreCli.Equals(dto.NombreCli, StringComparison.OrdinalIgnoreCase)))
            {
                return false; // O lanzar una excepción dependiendo de tu manejo de errores
            }

            var productDto = new ClientesDto
            {
                Id = Guid.NewGuid(),
                NombreCli = dto.NombreCli,
                Email = dto.Email,

            };
            productsDtos.Add(productDto);

            var productos = productsDtos.Select(x => new ClienteEntity
            {
                IdCliente = x.Id,
                NombreCli = x.NombreCli,
                Email = x.Email,  

            }).ToList();

            await WriteClientesToFileAsync(productos);
            return true;
        }

        public async Task<bool> DeleteClienteAsync(Guid id)
        {
            var clientesDto = await ReadClientesFromFileAsync();
            var clientesToDelete = clientesDto.FirstOrDefault(x => x.Id == id);
            if (clientesToDelete is null)
            {
                return false;
            }
            clientesDto.Remove(clientesToDelete);
            var products = clientesDto.Select(x => new ClienteEntity
            {
                IdCliente = x.Id,
                NombreCli= x.NombreCli,
                Email = x.Email,
            }).ToList();
            await WriteClientesToFileAsync(products);
            return true;
        }

        public async Task<bool> EditClienteAsync(ClienteEditDto dto, Guid id)
        {
            var clientesDto = await ReadClientesFromFileAsync();
            var existingcliente = clientesDto.FirstOrDefault(clientes => clientes.Id == id);
            if (existingcliente is null)
            {
                return false;
            }

            if (clientesDto.Any(p => p.NombreCli.Equals(dto.NombreCli, StringComparison.OrdinalIgnoreCase) && p.Id != id))
            {
                return false;
            }

            for (int i = 0; i < clientesDto.Count; i++)
            {
                if (clientesDto[i].Id == id)
                {
                    clientesDto[i].NombreCli = dto.NombreCli;
                    clientesDto[i].Email = dto.Email;
                }
            }

            var products = clientesDto.Select(x => new ClienteEntity
            {
             IdCliente = x.Id,
             NombreCli = x.NombreCli,
             Email = x.Email,
            }).ToList();

            await WriteClientesToFileAsync(products);
            return true;
        }

        public async Task<ClientesDto> GetClienteByIdAsync(Guid id)
        {
            var clientes = await ReadClientesFromFileAsync();
            ClientesDto cliente = clientes.FirstOrDefault(c => c.Id == id);
            return cliente;
        }

        public async Task<List<ClientesDto>> GetClientesListAsync()
        {
            return await ReadClientesFromFileAsync();
        }

        private async Task<List<ClientesDto>> ReadClientesFromFileAsync()
        {
            if (!File.Exists(_JSON_FILE))
            {
                return new List<ClientesDto>();
            }
            var json = await File.ReadAllTextAsync(_JSON_FILE);
            var clientes = JsonConvert.DeserializeObject<List<ClienteEntity>>(json);
            var dtos = clientes.Select(x => new ClientesDto
            {
               Id = x.IdCliente,
               NombreCli = x.NombreCli,
               Email = x.Email,
            }).ToList();

            return dtos;
        }
        private async Task WriteClientesToFileAsync(List<ClienteEntity> clientes)
        {
            var json = JsonConvert.SerializeObject(clientes, Formatting.Indented);

            if (File.Exists(_JSON_FILE))
            {
                await File.WriteAllTextAsync(_JSON_FILE, json);
            }
        }


    }
}

