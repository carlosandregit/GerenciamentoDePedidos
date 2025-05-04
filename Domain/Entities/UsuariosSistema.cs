using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDePedidosWebApi.Domain.Entities
{
    public class UsuariosSistema
    {
        [Key]
        public decimal IdRegistro { get; set; }
        public string IdUsuario { get; set; }
        public string Senha { get; set; }
    }
}
