using GerenciamentoDePedidosWebApi.Application.Interfaces;
using GerenciamentoDePedidosWebApi.Domain.Entities;
using GerenciamentoDePedidosWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDePedidosWebApi.Infrastructure.Repositories
{
    public class UsuarioRepository : Repository<UsuariosSistema>, IUsuarioRepository
    {
        public UsuarioRepository(DataContext context)
            : base(context)
        { }

        private DataContext _context
        {
            get { return Context as DataContext; }
        }

        public async Task<UsuariosSistema> GetUsuarioSistena(string usuario, string senha)
        {
            try
            {
                 return await _context.UsuariosSistema.FirstOrDefaultAsync(x => x.Usuario == usuario && x.Senha == senha);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuariosSistema> VerificaTokenAcesso(Guid token)
        {
            return await _context.UsuariosSistema.FirstOrDefaultAsync<UsuariosSistema>(x => x.TokenAcesso == token);
        }

        public async Task UpdateUsuariosSistema(UsuariosSistema usuariosSistema)
        {
            _context.UsuariosSistema.Update(usuariosSistema);
            await Context.SaveChangesAsync();
        }
        public async Task<UsuariosSistema> InsertUsuario(UsuariosSistema usuariosSistema)
        {
            try
            {
                await _context.UsuariosSistema.AddAsync(usuariosSistema);
                await Context.SaveChangesAsync();
                return usuariosSistema;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }
    }
}
