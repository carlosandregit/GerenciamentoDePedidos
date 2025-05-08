using System.Text.Json;
using GerenciamentoDePedidosWebApi.Application.Interfaces;
using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDePedidosWebApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ILogger<AutenticacaoController> _logger;

        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

        public ClientesController(IClienteService clienteService, ILogger<AutenticacaoController> logger)
        {
            _clienteService = clienteService;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("listar")]
        public async Task<IActionResult> GetAllCliente()
        {
            try
            {
                var response = await _clienteService.GetAllAsync();
                if (response.Count <= 0)
                {
                    var msg = $"404, Clientes/listar, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, - Nenhum cliente localizado";
                    _logger.LogInformation(msg);
                    return NotFound(new { mensagem = "Nenhum cliente localizado" });
                }
                else
                {   var msg = $"200,  Clientes/listar, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, - Clientes carregados com sucesso";
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
        public async Task<IActionResult> GetByIdCliente(int idCliente)
        {
            try
            {
                if(idCliente <= 0)
                {
                    var msg = $"400, Clientes/listar/id, {JsonSerializer.Serialize(idCliente, jsonSerializerOptions)}, {JsonSerializer.Serialize(idCliente, jsonSerializerOptions)}, - Cliente Id inválido";
                    _logger.LogInformation(msg);
                    return BadRequest(new {mensagen = "Cliente Id inválido"});
                }
                else
                {
                    var response = await _clienteService.GetClienteById(idCliente);
                    if (response == null)
                    {
                        var msg = $"404, Clientes/listar/id, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, - Nenhum cliente localizado";
                        _logger.LogInformation(msg);
                        return NotFound(new { mensagem = "Nenhum cliente localizado" });
                    }
                    else
                    {
                        var msg = $"200,  Clientes/listar/id, {JsonSerializer.Serialize(response, jsonSerializerOptions)}, {JsonSerializer.Serialize(response, jsonSerializerOptions)},  - Cliente carregado com sucesso";
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
        public async Task<IActionResult> CadastroCliente([FromBody] ClienteRequeste model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var clienteExiste = await _clienteService.GetClienteByCpf(model.CPF);
                    if (clienteExiste)
                    {
                        var msg = $"400, Clientes/cadastrar, {JsonSerializer.Serialize(model, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Cliente já cadastro para o cpf informado";
                        _logger.LogInformation(msg);
                        return BadRequest(new { mensagem = "Cliente já cadastro para o cpf informado" });
                    }
                    else
                    {
                        var response = await _clienteService.AdesaoCliente(model);
                        if (!string.IsNullOrEmpty(response))
                        {
                            var msg = $"200, Clientes/listar, {JsonSerializer.Serialize(response, jsonSerializerOptions)} - Cliente carregado com sucesso";
                            _logger.LogInformation(msg);
                            return response = Ok(response);
                        }
                        else
                        {
                            var msg = $"400, Clientes/cadastrar, {JsonSerializer.Serialize(model, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Erro ao cadastrar o cliente";
                            _logger.LogInformation(msg);
                            return BadRequest(new {mensagem = "Erro ao cadastrar o cliente" });
                        }
                    }
                    
                }
                else
                {
                    var msg = $"400, Clientes/cadastrar, {JsonSerializer.Serialize(model, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Falha na comunicação";
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
        public async Task<IActionResult> AtualizaCliente(int idCliente, [FromBody] ClienteRequeste model)
        {
            try
            {
                if (idCliente <= 0)
                {
                    var msg = $"400, Clientes/atualizar/id, {JsonSerializer.Serialize(model, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Cliente Id inválido";
                    _logger.LogInformation(msg);
                    return BadRequest(new { mensagen = "Cliente Id inválido" });
                }
                else
                {
                    var clienteExiste = await _clienteService.GetClienteById(idCliente);
                    if (clienteExiste == null)
                    {
                        var msg = $"404, Clientes/atualizar/id, {JsonSerializer.Serialize(model, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Cliente não localizado";
                        _logger.LogInformation(msg);
                        return NotFound(new { mensagem = "Cliente não localizado" });
                    }
                    else
                    {
                        var response = await _clienteService.AtualizarCliente(idCliente, model);

                        if (response != null)
                        {
                            var msg = $"200, Clientes/atualizar/id, {JsonSerializer.Serialize(response, jsonSerializerOptions)} - Atualizado com sucesso";
                            _logger.LogInformation(msg);
                            return Ok(response);
                        }
                        else
                        {
                            var msg = $"400 ,Clientes/atualizar/id, {JsonSerializer.Serialize(model, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Erro ao atualizar o cliente";
                            _logger.LogInformation(msg);
                            return BadRequest(new { mensagem = "Erro ao atualizar o cliente" });
                        }
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
        [HttpPost("deletar/id")]
        public async Task<IActionResult> DeletaCliente(int idCliente)
        {
            try
            {
                if (idCliente <= 0)
                {
                    var msg = $"400, Clientes/deletar/id, {JsonSerializer.Serialize(idCliente, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Cliente Id inválido";
                    _logger.LogInformation(msg);
                    return BadRequest(new { mensagen = "Cliente Id inválido" });
                }
                else
                {
                    var clienteExiste = await _clienteService.GetClienteById(idCliente);
                    if (clienteExiste == null)
                    {
                        var msg = $"404, Clientes/deletar/id, {JsonSerializer.Serialize(idCliente, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Cliente não localizado";
                        _logger.LogInformation(msg);
                        return NotFound(new { mensagem = "Cliente não localizado" });
                    }
                    else
                    {
                        var response = await _clienteService.DeleteCliente(idCliente);

                        if (response != null)
                        {
                            var msg = $"200, Clientes/deletar/id, {JsonSerializer.Serialize(response, jsonSerializerOptions)} - Cliente removido com sucesso";
                            _logger.LogInformation(msg);
                            return Ok(response);
                        }
                        else
                        {
                            var msg = $"400 ,Clientes/deletar/id, {JsonSerializer.Serialize(idCliente, jsonSerializerOptions)}, {JsonSerializer.Serialize(ModelState, jsonSerializerOptions)} - Erro ao deletar o cliente";
                            _logger.LogInformation(msg);
                            return BadRequest(new { mensagem = "Erro ao deletar o cliente" });
                        }
                    }
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
