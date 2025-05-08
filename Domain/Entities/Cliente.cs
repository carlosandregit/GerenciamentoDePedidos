using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GerenciamentoDePedidosWebApi.Domain.Entities
{
    [Table("clientes")] 
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_cliente")]
        public int IdCliente { get; set; } 

        [Required]
        [Column("nome_completo", TypeName = "varchar(100)")] 
        [MaxLength(100)]
        public string NomeCliente { get; set; }

        [Required]
        [Column("cpf", TypeName = "varchar(11)")]
        [MaxLength(11)]
        public string CPF { get; set; }

        [Column("email", TypeName = "varchar(100)")]
        [MaxLength(100)]
        [EmailAddress] 
        public string Email { get; set; }

        [Column("data_cadastro", TypeName = "timestamp with time zone")] 
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        [Column("data_nascimento", TypeName = "timestamp with time zone")]
        public DateTime DataNascimento { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>(); 
    }
}
