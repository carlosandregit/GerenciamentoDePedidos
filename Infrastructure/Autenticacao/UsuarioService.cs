using GerenciamentoDePedidosWebApi.Application.Interfaces;
using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Application.Models.Response;
using GerenciamentoDePedidosWebApi.Domain.Entities;

namespace GerenciamentoDePedidosWebApi.Infrastructure.Autenticacao
{
    public interface IUsuarioService
    {
        Task<AutenticacaoResponse> InsertUsuario(AutenticacaoRequest model);
        Task<AutenticacaoResponse> Login(AutenticacaoRequest model);
        Task<AutenticacaoResponse> RefreshToken(string token);
        Task<bool> RevokeToken(string token);
    }
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<AutenticacaoResponse> Login(AutenticacaoRequest model)
        {
            var credenciais = await _usuarioRepository.GetUsuarioSistena(model.Usuario, model.Senha);
            if (credenciais == null)
                return null;

            var jwt = TokenService.GenerateToken(credenciais);

            return new AutenticacaoResponse(credenciais, jwt, credenciais.TokenAcesso.ToString());
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
        public async Task<AutenticacaoResponse> InsertUsuario(AutenticacaoRequest model)
        {
            UsuariosSistema usuario = new UsuariosSistema()
            {        
                Usuario = model.Usuario,
                Senha = model.Senha,
                TokenAcesso = Guid.NewGuid(),
            };
            var credenciais = await _usuarioRepository.InsertUsuario(usuario);

           

            return new AutenticacaoResponse(credenciais, credenciais.TokenAcesso.ToString(), credenciais.TokenAcesso.ToString());
        }
    }
}
