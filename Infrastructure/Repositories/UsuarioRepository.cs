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

        public async Task<List<UsuariosSistema>> GetUsuarioSistena(string usuario, string senha)
        {
            return await _context.UsuariosSistema.Where(x => x.Usuario == usuario && x.Senha == senha).ToListAsync();
        }

        public async Task<UsuariosSistema> VerificaTokenAcesso(Guid token)
        {
            return await _context.UsuariosSistema.FirstOrDefaultAsync<UsuariosSistema>(x => x.TokenAcesso == token);
        }

        public async Task UpdateUsuariosSistema(UsuariosSistema usuariosSistema)
        {
            Context.Update(usuariosSistema);
            await Context.SaveChangesAsync();
        }
    }
}
