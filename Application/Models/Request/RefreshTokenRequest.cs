using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GerenciamentoDePedidosWebApi.Application.Models.Request
{
    public class RefreshTokenRequest
    {
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [DefaultValue("CdECC9a7-f34b-6Af0-db29-f7793eaF0eBe")]
        public string RefreshToken { get; set; }
    }
}
