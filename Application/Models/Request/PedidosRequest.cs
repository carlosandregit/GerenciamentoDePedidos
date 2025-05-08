using System.ComponentModel.DataAnnotations;
using GerenciamentoDePedidosWebApi.Application.Dto;

namespace GerenciamentoDePedidosWebApi.Application.Models.Request
{
    public class PedidosRequest
    {
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"\d{11}", ErrorMessage = "CPF deve conter 11 dígitos.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O pedido deve conter pelo menos um item.")]
        [MinLength(1, ErrorMessage = "O pedido deve conter pelo menos um item.")]
        public List<PedidoItemDto> Itens { get; set; }
    }
}
