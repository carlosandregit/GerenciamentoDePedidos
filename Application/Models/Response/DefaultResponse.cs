using System.ComponentModel;

namespace GerenciamentoDePedidosWebApi.Application.Models.Response
{
    public class DefaultResponse
    {
        public DefaultResponse(List<string> mensagem)
        {
            Mensagem = mensagem;

        }
        [DefaultValue("Mensagem de informação, crítica ou erro")]
        public List<string> Mensagem { get; set; }
    }
}
