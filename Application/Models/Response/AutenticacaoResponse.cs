using System.ComponentModel;
using GerenciamentoDePedidosWebApi.Domain.Entities;

namespace GerenciamentoDePedidosWebApi.Application.Models.Response
{
    public class AutenticacaoResponse
    {
        
        [DefaultValue("jose.souza")]
        public string Usuario { get; set; }
        
        [DefaultValue("eyJhbGciOiJIUzI1NiIsImtpZ.eyJuYW1lIjoiQWRtaW5pc3RyYXRvciIsInJvbGUiOiIxIiwibmJ.vXKIhnnNPqgs9rK-1EMODtD7SVDj2HDFLYsSbhKPFpQ")]
        public string AccessToken { get; set; }
       
        [DefaultValue("bearer")]
        public string TokenType { get; set; }
        
        [DefaultValue("8ae548ac-7611-4510-aaa8-9fc1bcea65de")]
        public string RefreshToken { get; set; }

        public AutenticacaoResponse(UsuariosSistema user, string jwtToken, string refreshToken)
        {
            Usuario = user.Usuario;
            TokenType = "bearer";
            AccessToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
