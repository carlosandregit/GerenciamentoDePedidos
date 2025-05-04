using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDePedidosWebApi.Application.Dto
{
    public class PedidoItemDto
    {
        [Required(ErrorMessage = "ProdutoId é obrigatório.")]
        public Guid ProdutoId { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que 0.")]
        public int Quantidade { get; set; }
    }
}
