using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Application.Models.Response;
using GerenciamentoDePedidosWebApi.Domain.Entities;

namespace GerenciamentoDePedidosWebApi.Application.Interfaces
{
    public interface IUsuarioRepository : IRepository<UsuariosSistema>
    {
        Task<UsuariosSistema> GetUsuarioSistena(string usuario, string senha);
        Task<UsuariosSistema> VerificaTokenAcesso(Guid token);
        Task UpdateUsuariosSistema(UsuariosSistema usuariosSistema);
        Task<UsuariosSistema> InsertUsuario(UsuariosSistema model);
    }
}
