using GerenciamentoDePedidosWebApi.Domain.Entities;

namespace GerenciamentoDePedidosWebApi.Application.Interfaces
{
    public interface IUsuarioRepository : IRepository<UsuariosSistema>
    {
        Task<List<UsuariosSistema>> GetUsuarioSistena(string usuario, string senha);
        Task<UsuariosSistema> VerificaTokenAcesso(Guid token);
        Task UpdateUsuariosSistema(UsuariosSistema usuariosSistema);
    }
}
