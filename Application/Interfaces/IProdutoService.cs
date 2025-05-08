using GerenciamentoDePedidosWebApi.Application.Dto;
using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Domain.Entities;

namespace GerenciamentoDePedidosWebApi.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<Produto> GetByIdAsync(int produtoId);
        Task<List<Produto>> GetAllAsync();
        Task<ProdutoDto> CadastrarProdutoAsync(ProdutoRequest model);
        Task<ProdutoDto> AtualizarProdutoAsync(int idProduto, ProdutoRequest model);
        Task<bool> DeletarProdutoAsync(int idProduto);
    }
}
