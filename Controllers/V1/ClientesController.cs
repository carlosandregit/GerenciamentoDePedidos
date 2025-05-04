using GerenciamentoDePedidosWebApi.Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDePedidosWebApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class ClientesController : ControllerBase
    {
        //private readonly IClienteService _clienteService;

        //public ClientesController(IClienteService clienteService)
        //{
        //    _clienteService = clienteService;
        //}
        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("listar")]
        public async Task<IActionResult> GetAllCliente()
        {
            //var clientes = await _clienteService.GetAllAsync();
            return Ok();
        }
        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("lista/{id}")]
        public async Task<IActionResult> GetByIdCliente(decimal id)
        {
            //var cliente = await _clienteService.GetByIdAsync(id);
            //if (cliente == null) return NotFound();
            return Ok();
        }
        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastroCliente([FromBody] ClienteRequeste model)
        {
            return BadRequest();
            //if (!ModelState.IsValid) return BadRequest(ModelState);

            //var cliente = await _clienteService.CreateAsync(dto);
            //return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }
        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("atualizar/{id}")]
        public async Task<IActionResult> AtualizaCliente(Guid id, [FromBody] ClienteRequeste model)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);

            //var updated = await _clienteService.UpdateAsync(id, dto);
            //if (!updated) return NotFound();
            return NoContent();
        }
        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("deletar/{id}")]
        public async Task<IActionResult> DeletaCliente(Guid id)
        {
            //var deleted = await _clienteService.DeleteAsync(id);
            //if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
