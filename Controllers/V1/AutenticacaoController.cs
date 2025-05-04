using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Application.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDePedidosWebApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        public AutenticacaoController()
        {

        }

     
        [ProducesResponseType(typeof(AutenticacaoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status401Unauthorized)]
        [MapToApiVersion("1.0")]
        [HttpPost("Login")]        
        public async Task<IActionResult> Authenticate([FromBody] AutenticacaoRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok();
                }
                else
                    return BadRequest(ModelState);

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [ProducesResponseType(typeof(AutenticacaoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status401Unauthorized)]
        [MapToApiVersion("1.0")]
        [HttpPost("Refres-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok();
                    //var response = await _usuarioService.RefreshToken(model.RefreshToken);
                    //if (response != null)
                    //    return Ok(response);
                    //else
                    //    return Unauthorized(new { message = "Token Inválido" });
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [ProducesResponseType(typeof(AutenticacaoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status401Unauthorized)]
        [MapToApiVersion("1.0")]
        [HttpPost("Logout")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            try
            {
                return Ok();
                //if (ModelState.IsValid)
                //{
                //    var response = await _usuarioService.RevokeToken(model.RefreshToken);
                //    if (response)
                //        return Ok(new { message = "Token revogado" });
                //    else
                //        return NotFound(new { message = "Token não encontrado" });
                //}
                //else
                //    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
