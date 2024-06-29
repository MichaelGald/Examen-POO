using Exame_POO.Dtos.Gestion.Clientes;
using Exame_POO.Dtos.Gestion.Pedidos;
using Exame_POO.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Exame_POO.Controllers
{
    [ApiController]
    [Route("api/Cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServicios _clienteServices;

        public ClienteController(IClienteServicios clientesServices)
        {
            this._clienteServices = clientesServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _clienteServices.GetClientesListAsync());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var cliente = await _clienteServices.GetClienteByIdAsync(id);

            if (cliente == null)
            {
                return NotFound(new { Message = $"No se encontro el Cliente: {id}" });
            }

            return Ok(cliente);
        }
        [HttpPost]
        public async Task<ActionResult> Create(ClientesCreateDto dto)
        {
            await _clienteServices.CreateClienteAsync(dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(ClienteEditDto dto, Guid id)
        {
            var result = await _clienteServices.EditClienteAsync(dto, id);

            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var cliente = await _clienteServices.GetClienteByIdAsync(id);

            if (cliente is null)
            {
                return NotFound();
            }

            await _clienteServices.DeleteClienteAsync(id);
            return Ok();
        }

    }
}

