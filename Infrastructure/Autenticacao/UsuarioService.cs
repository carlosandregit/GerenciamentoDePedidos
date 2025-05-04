using GerenciamentoDePedidosWebApi.Application.Facede;
using GerenciamentoDePedidosWebApi.Application.Interfaces;
using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Application.Models.Response;

namespace GerenciamentoDePedidosWebApi.Infrastructure.Autenticacao
{
    public interface IUsuarioService
    {
        Task<AutenticacaoResponse> Login(AutenticacaoRequest model);
        Task<AutenticacaoResponse> RefreshToken(string token);
        Task<bool> RevokeToken(string token);
    }
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _usuarioRepository;
        public async Task<AutenticacaoResponse> Login(AutenticacaoRequest model)
        {
            var credenciais = await _usuarioRepository.GetUsuarioSistena(model.Usuario, model.Senha);
            if (credenciais == null)
                return null;

            var jwt = TokenService.GenerateToken(credenciais[0]);

            return new AutenticacaoResponse(credenciais[0], jwt, credenciais[0].TokenAcesso.ToString());
        }

        public async Task<AutenticacaoResponse> RefreshToken(string token)
        {
            var credenciais = await _usuarioRepository.VerificaTokenAcesso(Guid.Parse(token));

            if (credenciais == null)
                return null;

            var jwtToken = TokenService.GenerateToken(credenciais);
            credenciais.TokenAcesso = Guid.NewGuid();
            await _usuarioRepository.UpdateUsuariosSistema(credenciais);

            return new AutenticacaoResponse(credenciais, jwtToken, credenciais.TokenAcesso.ToString());
        }

        public async Task<bool> RevokeToken(string token)
        {
            var credenciais = await _usuarioRepository.VerificaTokenAcesso(Guid.Parse(token));

            if (credenciais == null)
                return false;

            credenciais.TokenAcesso = null;

            await _usuarioRepository.UpdateUsuariosSistema(credenciais);

            return true;
        }
    }
}
