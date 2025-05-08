using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoDePedidosWebApi.Domain.Entities
{
    [Table("usuarios_sistema")]  
    public class UsuariosSistema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Required] 
        [Column("usuario", TypeName = "varchar(50)")]  
        [MaxLength(50)]  
        public string Usuario { get; set; }

        [Required]
        [Column("senha", TypeName = "varchar(255)")] 
        [MaxLength(255)]
        public string Senha { get; set; }

        [Column("token_acesso", TypeName = "uuid")] 
        public Guid? TokenAcesso { get; set; } = null; 
    }
}
