using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GerenciamentoDePedidosWebApi.Application.Models.Request
{
    public class AutenticacaoRequest
    {
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DefaultValue("andre.lima")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DefaultValue("1234")]
        public string Senha { get; set; }
    }
}
