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
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IClienteService _clienteService;
        private readonly ILogger <AutenticacaoController>_logger;

        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

        public PedidosController(IPedidoService pedidoService, ILogger<AutenticacaoController> logger, IClienteService clienteService)
        {
            _pedidoService = pedidoService;
            _logger = logger;
            _clienteService = clienteService;
        }

        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("listar")]
        public async Task<IActionResult> GetAllPedidos()
        {
            try
            {
                var response = await _pedidoService.GetAllAsync();
                if (response.Count <= 0)
                {
                    var msg = $"404, Pedidos/listar, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, - Nenhum Pedidos localizado";
                    _logger.LogInformation(msg);
                    return NotFound(new { mensagem = "Nenhum Pedido localizado" });
                }
                else
                {
                    var msg = $"200,  Pedidos/listar, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, - Pedido carregado com sucesso";
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
        [HttpPost("listar/id")]
        public async Task<IActionResult> GetByIdPedido(int idPedido)
        {
            try
            {
                if (idPedido <= 0)
                {
                    var msg = $"400, Pedidos/listar/id, {JsonSerializer.Serialize(idPedido, jsonSerializerOptions)}, {JsonSerializer.Serialize(idPedido, jsonSerializerOptions)}, - Pedido id inválido";
                    _logger.LogInformation(msg);
                    return BadRequest(new { mensagem = "Pedido id inválido" });
                }
                else
                {
                    var response = await _pedidoService.GetPedidoById(idPedido);
                    if (response == null)
                    {
                        var msg = $"404, Pedidos/listar/id, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, - Nenhum Pedidos localizado";
                        _logger.LogInformation(msg);
                        return NotFound(new { mensagem = "Nenhum Pedido localizado" });
                    }
                    else
                    {
                        var msg = $"200,  Pedidos/listar/id, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, - Pedido carregado com sucesso";
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
        public async Task<IActionResult> CadastroPedido([FromBody] PedidosRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {                 
                    var clienteExiste = await _clienteService.GetClienteByCpf(model.CPF);

                    if (!clienteExiste)
                    {
                        var msg = $"404, Pedidos/cadastrar, {JsonSerializer.Serialize(model, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Cliente não localizado";
                        _logger.LogInformation(msg);
                        return NotFound(new { mensagem = "Cliente não localizado" });
                    } 
                    else
                    {

                        var response = await _pedidoService.CadastrarPedido(model);
                        if (response != null)
                        {
                            var msg = $"200, Pedidos/cadastrar, {JsonSerializer.Serialize(response, jsonSerializerOptions)} - Cliente carregado com sucesso";
                            _logger.LogInformation(msg);
                            return  Ok(response);
                        }
                        else
                        {
                            var msg = $"404, Pedidos/cadastrar, {JsonSerializer.Serialize(model, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Produto não encontrado ou estoque insuficiente para o produto informado";
                            _logger.LogInformation(msg);
                            return NotFound(new { mensagem = "Produto não encontrado ou estoque insuficiente para o produto informado" });
                        }
                    }

                }
                else
                {
                    var msg = $"500, Pedidos/cadastrar, {JsonSerializer.Serialize(model, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Falha na comunicação";
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
        [HttpPost("atualizar/id")]
        public async Task<IActionResult> AtualizarPedido(int idPedido, [FromBody] UpdatePedidoRequest model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Itens[0].Quantidade <= 0)
                    {
                        var msg = $"400, Pedidos/atualizar/id, {JsonSerializer.Serialize(idPedido, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - O pedido deve conter pelo menos um produto";
                        _logger.LogInformation(msg);
                        return BadRequest("O pedido deve conter pelo menos um produto.");
                    }
                    var pedido = await _pedidoService.GetPedidoById(idPedido);
                    var cliente = await _clienteService.GetClienteByCpf(model.CPF);

                    if (pedido == null || cliente == false)
                    {
                        var msg = $"404, Pedidos/atualizar/id, {JsonSerializer.Serialize(idPedido, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Pedido não encontrado ou Cliente não encontrado.";
                        _logger.LogInformation(msg);
                        return NotFound("Pedido não encontrado ou Cliente não encontrado.");
                    }        

                    var response = await _pedidoService.AtualizarPedidoAsync(idPedido, model);

                    if (response != null)
                    {
                        var msg = $"200, Pedidos/atualizar/id, {JsonSerializer.Serialize(response, jsonSerializerOptions)} - Pedido atualizado com sucesso";
                        _logger.LogInformation(msg);
                        return Ok(response);
                    }
                    else
                    {
                        var msg = $"500, Pedidos/atualizar/id, {JsonSerializer.Serialize(idPedido, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - falha na comunicação, tente novamente!";
                        _logger.LogInformation(msg);
                        return BadRequest(new { mensagem = "falha na comunicação, tente novamente!" });
                    }
                }
                else
                {
                    var msg = $"500, Pedidos/atualizar/id, {JsonSerializer.Serialize(idPedido, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - falha na comunicação, tente novamente!";
                    _logger.LogInformation(msg);
                    return BadRequest(new { mensagem = "falha na comunicação, tente novamente!" });
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
        [HttpPost("deletar/id")]
        public async Task<IActionResult> DeletarPedido(int idPedido)
        {
            try
            {
                if (idPedido > 0)
                {
                    var response = await _pedidoService.DeletarPedidoAsync(idPedido);

                    if (!response)
                    {
                        var msg = $"404, Pedidos/deletar/id, {JsonSerializer.Serialize(idPedido, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Pedido não localizado";
                        _logger.LogInformation(msg);
                        return NotFound(new { mensagem = "Pedido não localizado" });
                    }
                    else
                    {
                        var msg = $"200, Pedidos/deletar/id, {JsonSerializer.Serialize(response, jsonSerializerOptions)} - Pedido excluido com sucesso";
                        _logger.LogInformation(msg);
                        return Ok(new { mensagem = "Pedido excluido com sucesso" });
                    }
                }
                else
                {
                    var msg = $"400, Pedidos/deletar/id, {JsonSerializer.Serialize(idPedido, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Id pedido inválido";
                    _logger.LogInformation(msg);
                    return BadRequest(new { mensagem = "Id pedido inválido" });
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
