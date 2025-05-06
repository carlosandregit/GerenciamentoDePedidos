using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDePedidosWebApi.Application.Models.Request
{
    public class ProdutoRequest
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        public string NomeProduto { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        public decimal PrecoProduto { get; set; }

        [Required(ErrorMessage = "Quantidade em estoque é obrigatório.")]
        public int Estoque { get; set; }
    }
}
