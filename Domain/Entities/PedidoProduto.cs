using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GerenciamentoDePedidosWebApi.Domain.Entities
{
    [Table("pedido_produtos")]  
    public class PedidoProduto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_pedido_produto")]
        public int IdPedidoProduto { get; set; }

        [Required]
        [Column("pedido_id")]
        public int PedidoId { get; set; }

        [ForeignKey("PedidoId")]
        [JsonIgnore]
        public virtual Pedido Pedido { get; set; } 

        [Required]
        [Column("produto_id")]
        public int ProdutoId { get; set; }

        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }  

        [Required]
        [Column("quantidade")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade mínima é 1")]
        public int Quantidade { get; set; } = 1;  

        [Required]
        [Column("preco_unitario", TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser positivo")]
        public decimal PrecoUnitario { get; set; }

        [Column("data_registro", TypeName = "timestamp with time zone")]  
        public DateTime DataDoPedido { get; set; } = DateTime.UtcNow;  
    }
}
