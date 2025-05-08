using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GerenciamentoDePedidosWebApi.Domain.Entities
{
    [Table("pedidos")] 
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_pedido")]
        public int IdPedido { get; set; }

        [Required]
        [Column("cliente_id")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; } 
        [Column("data_pedido", TypeName = "timestamp with time zone")] 
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;

        [Column("total_calculado", TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total deve ser positivo")]
        public decimal TotalCalculado { get; set; }

        [JsonIgnore]
        public virtual ICollection<PedidoProduto> PedidoProdutos { get; set; } = new List<PedidoProduto>();

        [NotMapped]
        public string NumeroPedido => $"PED-{IdPedido.ToString().PadLeft(8, '0')}";
    }
}
