using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDePedidosWebApi.Application.Dto
{
    public class PedidoItemDto
    {
        [Required(ErrorMessage = "ProdutoId é obrigatório.")]
        public decimal ProdutoId { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que 0.")]
        public int Quantidade { get; set; }
        public string DescricaoProduto { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório.")]
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal => Quantidade * PrecoUnitario;
    }
}
