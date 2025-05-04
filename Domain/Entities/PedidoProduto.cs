using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDePedidosWebApi.Domain.Entities
{
    public class PedidoProduto
    {
        [Key]
        public decimal IdPedidoProduto { get; set; }
        public decimal PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public decimal ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public DateTime DataDoPedido { get; set; }
    }
}
