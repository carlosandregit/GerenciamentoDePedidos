using GerenciamentoDePedidosWebApi.Domain.Entities;

namespace GerenciamentoDePedidosWebApi.Application.Interfaces
{
    public interface IPedidoRepository : IRepository<Pedido> 
    {
        Task<List<Pedido>> GetAllAsync();
        Task<Pedido> GetPedidoById(int idPedido);       
        Task<Pedido> CadastrarPedido(Pedido pedido);
        Task<dynamic> DeletarPedidoAsync(Pedido pedido);
        Task<dynamic> AtualizarPedidoAsync(Pedido pedido);
    }
}
