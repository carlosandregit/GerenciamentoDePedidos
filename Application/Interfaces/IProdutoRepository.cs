using GerenciamentoDePedidosWebApi.Application.Dto;
using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Domain.Entities;

namespace GerenciamentoDePedidosWebApi.Application.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> GetByIdAsync(int produtoId);
        Task<List<Produto>> GetAllAsync();
        Task<dynamic> CadastrarProdutoAsync(Produto produto);
        Task<dynamic> AtualizarProdutoAsync(Produto produto);
        Task<dynamic> DeletarProdutoAsync(Produto pedido);
    }
}
