using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDePedidosWebApi.Application.Dto
{
    public class UpdatePedidoItemDto
    {
        [Required(ErrorMessage = "O ProdutoId é obrigatório.")]
        public decimal ProdutoId { get; set; }
        [Required(ErrorMessage = "Deve conter pelo menos um produto")]
        public int Quantidade { get; set; }
    }
}
