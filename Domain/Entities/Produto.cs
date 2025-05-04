using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDePedidosWebApi.Domain.Entities
{
    public class Produto
    {
        [Key]
        public decimal IdProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public decimal PrecoProduto { get; set; }
        public int Estoque { get; set; }

        //public ICollection<PedidoProduto> PedidoProdutos { get; set; } = new List<PedidoProduto>();
    }
}
