using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDePedidosWebApi.Application.Models.Request
{
    public class ClienteRequeste
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"\d{11}", ErrorMessage = "CPF deve conter 11 dígitos.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }      

        [Required(ErrorMessage = "Data de Nascimento é obrigatório.")]
        [DefaultValue("yyyy-MM-dd")]
        public DateTime DataNascimento { get; set; }
    }
}
