using GerenciamentoDePedidosWebApi.Application.Interfaces;
using GerenciamentoDePedidosWebApi.Domain.Entities;
using GerenciamentoDePedidosWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDePedidosWebApi.Infrastructure.Repositories
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(DataContext context)
            : base(context) { }

        private DataContext _context
        {
            get { return Context as DataContext; }
        }

        public async Task<List<Pedido>> GetAllAsync()
        {
            return await _context.Pedidos.ToListAsync();
        }
        public async Task<Pedido> GetPedidoById(int idPedido)
        {
            return await _context.Pedidos.FindAsync(idPedido);            
        } 
        public async Task<Pedido> CadastrarPedido(Pedido pedido)
        {              
             await _context.Pedidos.AddAsync(pedido);            
             await _context.SaveChangesAsync(); 
             return pedido;
        } 
        public async Task<dynamic> DeletarPedidoAsync(Pedido pedido)
        {
            _context.Pedidos.Remove(pedido);
            return await _context.SaveChangesAsync();
        } 
        public async Task<dynamic> AtualizarPedidoAsync(Pedido pedido)
        {            
             _context.Pedidos.Update(pedido);
            return await _context.SaveChangesAsync();
        }


    }
}
