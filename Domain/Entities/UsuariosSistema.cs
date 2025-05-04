using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDePedidosWebApi.Domain.Entities
{
    public class UsuariosSistema
    {
        [Key]
        public decimal IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public Guid? TokenAcesso { get; set; }
    }
}
