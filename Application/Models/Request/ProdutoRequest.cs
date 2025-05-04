using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDePedidosWebApi.Application.Models.Request
{
    public class ProdutoRequest
    {
        //[Required(ErrorMessage = "O nome do produto é obrigatório.")]
        public string NomeProduto { get; set; }

        //[Required(ErrorMessage = "O email é obrigatório.")]
        public decimal PrecoProduto { get; set; }
        public int Estoque { get; set; }
    }
}
