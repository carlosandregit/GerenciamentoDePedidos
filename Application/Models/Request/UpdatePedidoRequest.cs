using System.ComponentModel.DataAnnotations;
using GerenciamentoDePedidosWebApi.Application.Dto;

namespace GerenciamentoDePedidosWebApi.Application.Models.Request
{
    public class UpdatePedidoRequest
    {
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"\d{11}", ErrorMessage = "CPF deve conter 11 dígitos.")]
        public string CPF { get; set; }
        public List<UpdatePedidoItemDto> Itens { get; set; }
    }
}
