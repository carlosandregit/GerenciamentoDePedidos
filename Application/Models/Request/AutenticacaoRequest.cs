using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GerenciamentoDePedidosWebApi.Application.Models.Request
{
    public class AutenticacaoRequest
    {
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DefaultValue("fulano.fulano")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DefaultValue("123453343232")]
        public string Senha { get; set; }
    }
}
