using GerenciamentoDePedidosWebApi.Application.Dto;
using GerenciamentoDePedidosWebApi.Application.Interfaces;
using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Domain.Entities;
using GerenciamentoDePedidosWebApi.Infrastructure.Repositories;

namespace GerenciamentoDePedidosWebApi.Application.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoService(IProdutoRepository produtoRepository) 
        { 
            _produtoRepository = produtoRepository;
        }

        public async Task<List<Produto>> GetAllAsync()
        {
           return await _produtoRepository.GetAllAsync(); 
        }
        public async Task<Produto> GetByIdAsync(int produtoId)
        {
           return await _produtoRepository.GetByIdAsync(produtoId);
        }

        public async Task<ProdutoDto> CadastrarProdutoAsync(ProdutoRequest model)
        {
            var produto = new Produto
            {
                DescricaoProduto = model.NomeProduto,
                PrecoProduto = model.PrecoProduto,
                Estoque = model.Estoque
            };

            await _produtoRepository.CadastrarProdutoAsync(produto);

            return new ProdutoDto
            {
                IdProduto = produto.IdProduto,
                DescricaoProduto = produto.DescricaoProduto,
                PrecoProduto = produto.PrecoProduto,
                Estoque = produto.Estoque
            };
        }

        public async Task<ProdutoDto> AtualizarProdutoAsync(int idProduto, ProdutoRequest model)
        {
            Produto produto = new Produto
            {
                IdProduto = idProduto,
                DescricaoProduto = model.NomeProduto,
                PrecoProduto = model.PrecoProduto,
                Estoque = model.Estoque,
            };     

            await _produtoRepository.AtualizarProdutoAsync(produto);

            return new ProdutoDto
            {
                IdProduto = produto.IdProduto,
                DescricaoProduto = produto.DescricaoProduto,
                PrecoProduto = produto.PrecoProduto,
                Estoque = produto.Estoque
            };
        }

        public async Task<bool> DeletarProdutoAsync(int idProduto)
        {
            var produto = await _produtoRepository.GetByIdAsync(idProduto);
            if (produto == null)
                return false;

           
            await _produtoRepository.DeletarProdutoAsync(produto);

            return true;
        }

    }
}
