using GerenciamentoDePedidosWebApi.Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDePedidosWebApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class ProdutosController : ControllerBase
    {
        //private readonly IProdutoService _produtoService;
        public ProdutosController()
        {
        }

        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("listar")]
        public async Task<IActionResult> GetAllProduto()
        {
            //var produtos = await _produtoService.GetAllAsync();
            return Ok();
        }

        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("listar/{id}")]
        public async Task<IActionResult> GetByIdProduto(decimal id)
        {
            //var produto = await _produtoService.GetByIdAsync(id);
            //if (produto == null) return NotFound();

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastroProduto([FromBody] ProdutoRequest model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok();
            //var produto = await _produtoService.CreateAsync(dto);
            //return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }
        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("atualizar/{id}")]
        public async Task<IActionResult> AtualizaProduto(Guid id, [FromBody] ProdutoRequest dto)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);

            //var updated = await _produtoService.UpdateAsync(id, dto);
            //if (!updated) return NotFound();

            return NoContent();
        }

        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("deletar/{id}")]
        public async Task<IActionResult> ApagaProduto(decimal id)
        {
            //var deleted = await _produtoService.DeleteAsync(id);
            //if (!deleted)
                //return NotFound();

            return NoContent();
        }
    }
}
