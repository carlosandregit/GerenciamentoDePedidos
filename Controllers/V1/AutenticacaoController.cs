using System.Text.Json;
using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Application.Models.Response;
using GerenciamentoDePedidosWebApi.Infrastructure.Autenticacao;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDePedidosWebApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<AutenticacaoController> _logger;

        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
        public AutenticacaoController(IUsuarioService usuarioService, ILogger<AutenticacaoController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpPost("Cadastro/usuario")]
        public async Task<IActionResult> CadastroUsuario([FromBody] AutenticacaoRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    

                    var response = await _usuarioService.InsertUsuario(model);

                    if (response != null)
                    {
                        _logger.LogInformation("404, Autenticacao/logout, " + JsonSerializer.Serialize(model, jsonSerializerOptions), JsonSerializer.Serialize(response, jsonSerializerOptions), "Token não encontrado");
                        return NotFound(new { message = "Token não encontrado" });
                    }
                    else
                    {
                        _logger.LogInformation("200, Autenticacao/logout, " + JsonSerializer.Serialize(model, jsonSerializerOptions), JsonSerializer.Serialize(response, jsonSerializerOptions), "Token revogado");
                        return Ok(new { message = "Token revogado" });
                    }
                }
                else
                {
                    _logger.LogInformation("400,  Autenticacao/logout, " + JsonSerializer.Serialize(model, jsonSerializerOptions), JsonSerializer.Serialize(ModelState, jsonSerializerOptions), "Falha na comunicação");
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocorreu uma falha na comunicação, tente novamente!");
                return BadRequest(new { mensagem = "Ocorreu uma falha na comunicação, tente novamente!" });
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate([FromBody] AutenticacaoRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _usuarioService.Login(model);

                    if (response == null)
                    {
                        _logger.LogInformation("400, Autenticacao/login, " + JsonSerializer.Serialize(model, jsonSerializerOptions), JsonSerializer.Serialize(response, jsonSerializerOptions), "Usuário ou Senha inválidos");
                        return BadRequest(new { message = "Usuário ou Senha inválidos" });
                    }
                    else
                    {
                        _logger.LogInformation("200, Autenticacao/login, " + JsonSerializer.Serialize(model, jsonSerializerOptions), JsonSerializer.Serialize(response, jsonSerializerOptions), "Confirmação de login com sucesso");
                        return Ok(response);
                    }
                }
                else 
                {
                    _logger.LogInformation("400, Autenticacao/login, " + JsonSerializer.Serialize(model, jsonSerializerOptions), JsonSerializer.Serialize(ModelState, jsonSerializerOptions), "Falha na comunicação");
                    return BadRequest(ModelState);
                }               

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocorreu uma falha na comunicação, tente novamente!");
                _logger.LogCritical("Ocorreu uma falha na comunicação, tente novamente!");
                //_logger.LogInformation("Ocorreu uma falha na comunicação, tente novamente!");
                return BadRequest(new { mensagem = "Ocorreu uma falha na comunicação, tente novamente!" });
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost("Refres-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var refreshToken = model.RefreshToken;
                    var response = await _usuarioService.RefreshToken(refreshToken);

                    if (response == null)
                    {
                        _logger.LogInformation("401, Autenticacao/refresh-token, " + JsonSerializer.Serialize(model, jsonSerializerOptions), JsonSerializer.Serialize(response, jsonSerializerOptions), "token inválido");
                        return Unauthorized(new { message = "Token Inválido" });
                    }
                    else
                    {
                        _logger.LogInformation("200, Autenticacao/refresh-token, " + JsonSerializer.Serialize(model, jsonSerializerOptions), JsonSerializer.Serialize(response, jsonSerializerOptions), "Confirmação de atualização do token com sucesso"); 
                        return Ok(response);
                    }
                }
                else
                {
                    _logger.LogInformation("400, Autenticacao/refresh-token, " + JsonSerializer.Serialize(model, jsonSerializerOptions), JsonSerializer.Serialize(ModelState, jsonSerializerOptions), "Falha na comunicação");
                    return BadRequest(ModelState);
                }
                    
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocorreu uma falha na comunicação, tente novamente!");
                return BadRequest(new { mensagem = "Ocorreu uma falha na comunicação, tente novamente!" });
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost("Logout")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var token = model.RefreshToken;

                    var response = await _usuarioService.RevokeToken(token);

                    if (!response)
                    {
                        _logger.LogInformation("404, Autenticacao/logout, " + JsonSerializer.Serialize(model, jsonSerializerOptions), JsonSerializer.Serialize(response, jsonSerializerOptions), "Token não encontrado");
                        return NotFound(new { message = "Token não encontrado" });
                    }
                    else
                    {
                        _logger.LogInformation("200, Autenticacao/logout, " + JsonSerializer.Serialize(model, jsonSerializerOptions), JsonSerializer.Serialize(response, jsonSerializerOptions), "Token revogado");
                        return Ok(new { message = "Token revogado" });
                    }
                }
                else
                {
                    _logger.LogInformation("400,  Autenticacao/logout, " + JsonSerializer.Serialize(model, jsonSerializerOptions), JsonSerializer.Serialize(ModelState, jsonSerializerOptions), "Falha na comunicação");
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                    _logger.LogInformation("Ocorreu uma falha na comunicação, tente novamente!");
                    return BadRequest(new { mensagem = "Ocorreu uma falha na comunicação, tente novamente!" });
            }
        }
    }
}
