using GerenciamentoDePedidosWebApi.Application.Interfaces;
using GerenciamentoDePedidosWebApi.Domain.Entities;
using GerenciamentoDePedidosWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDePedidosWebApi.Infrastructure.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DataContext context)
            : base(context) { }

        private DataContext _context
        {
            get { return Context as DataContext; }
        }

        public async Task<List<Produto>> GetAllAsync()
        {
             await _context.Produtos.FindAsync();
            return await _context.Produtos.ToListAsync();
        }
        
        public async Task<Produto> GetByIdAsync(decimal produtoId)
        {
            return await _context.Produtos.FindAsync(produtoId);
        } 
        
        public async Task<dynamic> CadastrarProdutoAsync(Produto produto)
        {
             await _context.Produtos.AddAsync(produto);
            return _context.SaveChangesAsync();

        }
        public async Task<dynamic> AtualizarProdutoAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            return await _context.SaveChangesAsync();
        }

        public async Task<dynamic> DeletarProdutoAsync(Produto produto)
        {
            _context.Produtos.Remove(produto);
            return await _context.SaveChangesAsync();
        }
    }
}
