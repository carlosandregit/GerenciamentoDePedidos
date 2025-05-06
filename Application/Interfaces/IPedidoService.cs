using System;
using GerenciamentoDePedidosWebApi.Application.Dto;
using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Domain.Entities;

namespace GerenciamentoDePedidosWebApi.Application.Interfaces
{
    public interface IPedidoService 
    {
        Task<List<Pedido>> GetAllAsync();
        Task<Pedido> GetPedidoById(decimal idPedido);
        Task<PedidoDto> CadastrarPedido(PedidosRequest model);
        Task<bool> DeletarPedidoAsync(decimal idPedido);
        Task<PedidoDto> AtualizarPedidoAsync(decimal idPedido, UpdatePedidoRequest model);

    }
}
