using System.Text.Json;
using GerenciamentoDePedidosWebApi.Application.Interfaces;
using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Application.Service;
using GerenciamentoDePedidosWebApi.Domain.Entities;
using GerenciamentoDePedidosWebApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDePedidosWebApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly ILogger<AutenticacaoController> _logger;

        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

        public ProdutosController(IProdutoService produtoService, ILogger<AutenticacaoController> logger)
        {
            _produtoService = produtoService;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("listar")]
        public async Task<IActionResult> GetAllProduto()
        {
            try
            {
                var response = await _produtoService.GetAllAsync();
                if (response.Count <= 0)
                {
                    var msg = $"404, Produtos/listar, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, - Nenhum produto localizado";
                    _logger.LogInformation(msg);
                    return NotFound(new { mensagem = "Nenhum produto localizado" });
                }
                else
                {
                    var msg = $"200,  Produtos/listar, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, - produto carregado com sucesso";
                    _logger.LogInformation(msg);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("500, Ocorreu uma falha na comunicação, tente novamente!");
                return BadRequest(new { mensagem = "Ocorreu uma falha na comunicação, tente novamente!" });
            }
        }

        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("listar/{id}")]
        public async Task<IActionResult> GetByIdProduto(decimal idProduto)
        {
            try
            {
                if (idProduto <= 0)
                {
                    var msg = $"400, produto/listar/id, {JsonSerializer.Serialize(idProduto, jsonSerializerOptions)}, {JsonSerializer.Serialize(idProduto, jsonSerializerOptions)}, - produto id inválido";
                    _logger.LogInformation(msg);
                    return BadRequest(new { mensagem = "produto id inválido" });
                }
                else
                {
                    var response = await _produtoService.GetByIdAsync(idProduto);
                    if (response == null)
                    {
                        var msg = $"404, produto/listar/id, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, - Nenhum produto localizado";
                        _logger.LogInformation(msg);
                        return NotFound(new { mensagem = "Nenhum produto localizado" });
                    }
                    else
                    {
                        var msg = $"200,  produto/listar/id, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, - produto carregado com sucesso";
                        _logger.LogInformation(msg);
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("500, Ocorreu uma falha na comunicação, tente novamente!");
                return BadRequest(new { mensagem = "Ocorreu uma falha na comunicação, tente novamente!" });
            }
        }

        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastroProduto([FromBody] ProdutoRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var response = await _produtoService.CadastrarProdutoAsync(model);
                    if (response != null)
                    {
                        var msg = $"200, produto/cadastrar, {JsonSerializer.Serialize(response, jsonSerializerOptions)} - produto cadastrado com sucesso";
                        _logger.LogInformation(msg);
                        return Ok(response);
                    }
                    else
                    {
                        var msg = $"400, Clientes/cadastrar, {JsonSerializer.Serialize(model, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Erro ao cadastrar o produto";
                        _logger.LogInformation(msg);
                        return BadRequest(new { mensagem = "Erro ao cadastrar o produto" });
                    }

                }
                else
                {
                    var msg = $"500, produto/cadastrar, {JsonSerializer.Serialize(model, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Falha na comunicação";
                    _logger.LogInformation(msg);
                    return BadRequest(new { mensagem = "Falha na comunicação" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("500, Ocorreu uma falha na comunicação, tente novamente!");
                return BadRequest(new { mensagem = "Ocorreu uma falha na comunicação, tente novamente!" });
            }
        }
        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("atualizar/{id}")]
        public async Task<IActionResult> AtualizaProduto(decimal idProduto, [FromBody] ProdutoRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (idProduto <= 0)
                    {
                        var msg = $"400, Pedidos/atualizar/id, {JsonSerializer.Serialize(idProduto, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - O pedido deve conter pelo menos um produto";
                        _logger.LogInformation(msg);
                        return BadRequest("idProduto deve ser maior q 0");
                    }
                    else
                    {
                        var produto = await _produtoService.GetByIdAsync(idProduto);
                        if (produto == null)
                        {
                            var msg = $"404, produto/atualizar, {JsonSerializer.Serialize(model, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - produto não localizado";
                            _logger.LogInformation(msg);
                            return NotFound(new { mensagem = "produto não localizado" });
                        }

                        var response = await _produtoService.AtualizarProdutoAsync(idProduto, model);
                        if (response == null)
                        {
                            var msg = $"400, produto/atualizar, {JsonSerializer.Serialize(model, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - erro na atualização";
                            _logger.LogInformation(msg);
                            return BadRequest(new { mensagem = "erro na atualização" });
                        }
                        else
                        {
                            var msg = $"200, produto/atualizar, {JsonSerializer.Serialize(response, jsonSerializerOptions)} - atualizado com sucesso";
                            _logger.LogInformation(msg);
                            return Ok(response);
                        }
                    }
                }
                else
                {
                    var msg = $"500, produto/atualizar, {JsonSerializer.Serialize(model, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Falha na comunicação";
                    _logger.LogInformation(msg);
                    return BadRequest(new { mensagem = "Falha na comunicação" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("500, Ocorreu uma falha na comunicação, tente novamente!");
                return BadRequest(new { mensagem = "Ocorreu uma falha na comunicação, tente novamente!" });
            }
        }

        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("deletar/{id}")]
        public async Task<IActionResult> ApagaProduto(decimal idProduto)
        {
            try
            {
                if (idProduto > 0)
                {
                    var response = await _produtoService.DeletarProdutoAsync(idProduto);

                    if (!response)
                    {
                        var msg = $"404, produto/deletar/id, {JsonSerializer.Serialize(idProduto, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - produto não localizado";
                        _logger.LogInformation(msg);
                        return NotFound(new { mensagem = "produto não localizado" });
                    }
                    else
                    {
                        var msg = $"200, produto/deletar/id, {JsonSerializer.Serialize(response, jsonSerializerOptions)} - produto excluido com sucesso";
                        _logger.LogInformation(msg);
                        return Ok(new { mensagem = "produto excluido com sucesso" });
                    }
                }
                else
                {
                    var msg = $"400, produto/deletar/id, {JsonSerializer.Serialize(idProduto, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Id produto inválido";
                    _logger.LogInformation(msg);
                    return BadRequest(new { mensagem = "Id produto inválido" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("500, Ocorreu uma falha na comunicação, tente novamente!");
                return BadRequest(new { mensagem = "Ocorreu uma falha na comunicação, tente novamente!" });
            }
        }
    }
}
