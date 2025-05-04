using GerenciamentoDePedidosWebApi.Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDePedidosWebApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class PedidosController : ControllerBase
    {
        //private readonly IPedidoService _pedidoService;

        //public PedidosController(IPedidoService pedidoService)
        //{
        //    _pedidoService = pedidoService;
        //}
        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("listar")]
        public async Task<IActionResult> GetAllPedidos()
        {
            //var pedidos = await _pedidoService.GetAllAsync();
            return Ok();
        }
        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("lista/{id}")]
        public async Task<IActionResult> GetByIdPedido(decimal id)
        {
            //var pedido = await _pedidoService.GetByIdAsync(id);
            //if (pedido == null) return NotFound();
            return Ok();
        }
        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastroPedido([FromBody] PedidosRequest model)
        {
            return Ok();
            //if (!ModelState.IsValid) return BadRequest(ModelState);

            //if (dto.Itens == null || !dto.Itens.Any())
            //    return BadRequest("Pedido deve conter pelo menos um item.");

            //var pedido = await _pedidoService.CreateAsync(dto);
            //return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
        }
        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("deletar/{id}")]
        public async Task<IActionResult> DeletarPedido(Guid id)
        {
            //var deleted = await _pedidoService.DeleteAsync(id);
            //if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
