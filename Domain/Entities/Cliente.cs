using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDePedidosWebApi.Domain.Entities
{
    public class Cliente
    {
        [Key]
        public decimal IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataNascimento { get; set; }
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    }
}
