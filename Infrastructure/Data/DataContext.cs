using GerenciamentoDePedidosWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDePedidosWebApi.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        
        }

        public DbSet<UsuariosSistema> UsuariosSistema { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoProduto> PedidoProdutos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        protected DataContext()
        {
        }
    }
}
