using GerenciamentoDePedidosWebApi.Application.Dto;
using GerenciamentoDePedidosWebApi.Application.Interfaces;
using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Domain.Entities;
using GerenciamentoDePedidosWebApi.Infrastructure.Repositories;

namespace GerenciamentoDePedidosWebApi.Application.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IClienteRepository _clienteRepository;
        public PedidoService(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository, IClienteRepository clienteRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
            _clienteRepository = clienteRepository;
        }
        public async Task<List<Pedido>> GetAllAsync() 
        {
            return await _pedidoRepository.GetAllAsync();
        }
        public async Task<Pedido> GetPedidoById(int idPedido)
        {
            return await _pedidoRepository.GetPedidoById(idPedido);
        } 
        public async Task<PedidoDto> CadastrarPedido(PedidosRequest model)
        {
            
            var cliente = await _clienteRepository.GetClienteByCpf(model.CPF);
            var pedido = new Pedido
            {
                ClienteId = cliente.IdCliente,
                DataPedido = DateTime.Now,
                PedidoProdutos = new List<PedidoProduto>()
            };

            decimal total = 0;

            foreach (var item in model.Itens)
            {
                var produto = await _produtoRepository.GetByIdAsync(item.ProdutoId);
                if (produto == null)
                    return  null; 

                if (produto.Estoque < item.Quantidade)
                    return null; 

            
                produto.Estoque -= item.Quantidade;

                var pedidoProduto = new PedidoProduto
                {
                    ProdutoId = produto.IdProduto,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = produto.PrecoProduto
                };

                pedido.PedidoProdutos.Add(pedidoProduto);
                total += item.Quantidade * produto.PrecoProduto;
            }

            pedido.TotalCalculado = total;

            await _pedidoRepository.CadastrarPedido(pedido);           

            return new PedidoDto
            {
                PedidoId = pedido.IdPedido,
                ClienteId = pedido.ClienteId,
                Total = pedido.TotalCalculado,
                DataCriacao = pedido.DataPedido,
                ProdutoId = model.Itens[0].ProdutoId,
                Quantidade = model.Itens[0].Quantidade,
            };
;
        }

        public async Task<bool> DeletarPedidoAsync(int idPedido)
        {
            var pedido = await _pedidoRepository.GetPedidoById(idPedido);
            if (pedido == null)
                return false;
      
            foreach (var item in pedido.PedidoProdutos)
            {
                var produto = await _produtoRepository.GetByIdAsync(item.ProdutoId);
                if (produto != null)
                    produto.Estoque += item.Quantidade;
            }

           await _pedidoRepository.DeletarPedidoAsync(pedido);  

           return true;            
        }

        public async Task<PedidoDto> AtualizarPedidoAsync(int idPedido, UpdatePedidoRequest model)
        {
            var pedido = await _pedidoRepository.GetPedidoById(idPedido);
            var cliente = await _clienteRepository.GetClienteByCpf(model.CPF);
            //var cliente = await _clienteRepository.GetClienteById(model.ClienteId);
    
            foreach (var itemAnterior in pedido.PedidoProdutos)
            {
                var produto = await _produtoRepository.GetByIdAsync(itemAnterior.ProdutoId);
                if (produto != null)
                    produto.Estoque += itemAnterior.Quantidade;
            }

            pedido.PedidoProdutos.Clear();

            decimal total = 0;

            foreach (var item in model.Itens)
            {
                var produto = await _produtoRepository.GetByIdAsync(item.ProdutoId);
                if (produto == null)
                    throw new Exception($"Produto {item.ProdutoId} não encontrado.");

                if (produto.Estoque < item.Quantidade)
                    throw new Exception($"Estoque insuficiente para o produto: {produto.DescricaoProduto}");

                produto.Estoque -= item.Quantidade;

                var novoItem = new PedidoProduto
                {
                    PedidoId = pedido.IdPedido,
                    ProdutoId = produto.IdProduto,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = produto.PrecoProduto
                };

                pedido.PedidoProdutos.Add(novoItem);
                total += item.Quantidade * produto.PrecoProduto;
            }

            pedido.ClienteId = cliente.IdCliente;
            pedido.TotalCalculado = total;
            pedido.DataPedido = DateTime.UtcNow;

            await _pedidoRepository.AtualizarPedidoAsync(pedido);

            return new PedidoDto
            {
                PedidoId = pedido.IdPedido,
                ClienteId = pedido.ClienteId,
                Total = pedido.TotalCalculado,
                DataCriacao = pedido.DataPedido,
                Itens = pedido.PedidoProdutos.Select(pp => new PedidoItemDto
                {
                    ProdutoId = pp.ProdutoId,
                    Quantidade = pp.Quantidade,
                }).ToList()
            };
        }

    }
}
