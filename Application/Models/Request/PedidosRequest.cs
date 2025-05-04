using System.ComponentModel.DataAnnotations;
using GerenciamentoDePedidosWebApi.Application.Dto;

namespace GerenciamentoDePedidosWebApi.Application.Models.Request
{
    public class PedidosRequest
    {
        [Required(ErrorMessage = "O ClienteId é obrigatório.")]
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "O pedido deve conter pelo menos um item.")]
        [MinLength(1, ErrorMessage = "O pedido deve conter pelo menos um item.")]
        public List<PedidoItemDto> Itens { get; set; }
    }
}
