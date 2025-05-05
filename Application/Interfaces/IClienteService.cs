using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Domain.Entities;

namespace GerenciamentoDePedidosWebApi.Application.Interfaces
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAllAsync();
        Task<Cliente> GetClienteById(decimal idCliente);       
        Task<dynamic> AdesaoCliente(ClienteRequeste model);
        Task<bool> GetClienteByCpf(string cpf);
        Task<Cliente> AtualizarCliente(decimal idCliente, ClienteRequeste model);
        Task<dynamic> DeleteCliente(decimal idCliente);

    }
}
