using Exame_POO.Dtos.Gestion.Pedidos;
using Exame_POO.Dtos.Gestion.Productos;
using Exame_POO.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Exame_POO.Controllers
{
       [ApiController]
       [Route("api/Pedido")]
    public class PedidosController : Controller
    {
            private readonly IPedidoServicios _pedidosServices;

            public PedidosController(IPedidoServicios pedidosServices)
            {
                this._pedidosServices = pedidosServices;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                return Ok(await _pedidosServices.GetPedidosListAsync());
            }

            [HttpGet("{Id}")]
            public async Task<ActionResult> Get(Guid id)
            {
                var product = await _pedidosServices.GetPedidoByIdAsync(id);

                if (product == null)
                {
                    return NotFound(new { Message = $"No se encontro el pedido: {id}" });
                }

                return Ok(product);
            }
            [HttpPost]
            public async Task<ActionResult> Create(PedidoCreateDto dto)
            {
                await _pedidosServices.CreatePedidoAsync(dto);
                return StatusCode(201);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult> Edit(PedidoEditDto dto, Guid id)
            {
                var result = await _pedidosServices.EditPedidoAsync(dto, id);

                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(Guid id)
            {
                var product = await _pedidosServices.GetPedidoByIdAsync(id);

                if (product is null)
                {
                    return NotFound();
                }

                await _pedidosServices.DeletePedidoAsync(id);
                return Ok();
            }

        }
    }

