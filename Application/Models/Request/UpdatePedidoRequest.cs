using GerenciamentoDePedidosWebApi.Application.Dto;

namespace GerenciamentoDePedidosWebApi.Application.Models.Request
{
    public class UpdatePedidoRequest
    {
        public decimal ClienteId { get; set; }
        public List<UpdatePedidoItemDto> Itens { get; set; }
    }
}
