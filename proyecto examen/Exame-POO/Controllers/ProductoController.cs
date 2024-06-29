using Exame_POO.Dtos.Gestion.Pedidos;
using Exame_POO.Dtos.Gestion.Productos;
using Exame_POO.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Exame_POO.Controllers
{
   
        [ApiController]
        [Route("api/Producto")]
        public class ProductoController : Controller
        {
            private readonly IProductoServicios _productoServices;

            public ProductoController(IProductoServicios productServices)
            {
                this._productoServices = productServices;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                return Ok(await _productoServices.GetProductosListAsync());
            }

            [HttpGet("{Id}")]
            public async Task<ActionResult> Get(Guid id)
            {
                var product = await _productoServices.GetProductoByIdAsync(id);

                if (product == null)
                {
                    return NotFound(new { Message = $"No se encontro el producto: {id}" });
                }

                return Ok(product);
            }
            [HttpPost]
            public async Task<ActionResult> Create(ProductoCreateDto dto)
            {
                await _productoServices.CreateProductoAsync(dto);
                return StatusCode(201);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult> Edit(ProductoEditDto dto, Guid id)
            {
                var result = await _productoServices.EditProductoAsync(dto, id);

                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(Guid id)
            {
                var product = await _productoServices.GetProductoByIdAsync(id);

                if (product is null)
                {
                    return NotFound();
                }

                await _productoServices.DeleteProdctoAsync(id);
                return Ok();
            }

        }
    }


