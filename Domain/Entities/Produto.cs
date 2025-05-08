using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDePedidosWebApi.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    [Table("produtos")]  
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_produto")]
        public int IdProduto { get; set; }

        [Required]
        [Column("descricao", TypeName = "varchar(100)")]  
        [MaxLength(100)]
        public string DescricaoProduto { get; set; }

        [Column("preco", TypeName = "decimal(18,2)")]  
        public decimal PrecoProduto { get; set; }

        [Column("estoque")]
        public int Estoque { get; set; } = 0;  

        [JsonIgnore]
        public virtual ICollection<PedidoProduto> PedidoProdutos { get; set; } = new List<PedidoProduto>();
    }
}
