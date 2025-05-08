using System.ComponentModel.DataAnnotations;
using GerenciamentoDePedidosWebApi.Application.Interfaces;
using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace GerenciamentoDePedidosWebApi.Application.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }


        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _clienteRepository.GetAllAsync();
        }

        public async Task<Cliente> GetClienteById(int idCliente)
        {
            return await _clienteRepository.GetClienteById(idCliente);
        }

        public async Task<dynamic> AdesaoCliente(ClienteRequeste model)
        {
            var mensagem = string.Empty;
            var cpf = model.CPF.Replace(".", "").Replace("-","");
            var dtCadastro = DateTime.Now;    
            var adesaoCliente = await _clienteRepository.AdesaoCliente(model.Nome, cpf, model.Email, model.DataNascimento, dtCadastro);

            if (adesaoCliente.IdCliente != 0)
                mensagem = "Cliente inserido com sucesso";

            return mensagem;
        }

        public async Task<bool> GetClienteByCpf(string cpf)
        {
            var cliente = await _clienteRepository.GetClienteByCpf(cpf);
            if (cliente == null) 
                return false;

            return true;
        }

        public async Task<Cliente> AtualizarCliente(int idCliente, ClienteRequeste model)
        {
            var cpf = model.CPF.Replace(".", "").Replace("-", "");
            var dtCadastro = DateTime.Now;
            return await _clienteRepository.AtualizarCliente(idCliente, model.Nome, cpf, model.Email, model.DataNascimento, dtCadastro);
        }
        
        public async Task<dynamic> DeleteCliente(int idCliente)
        {            
            await _clienteRepository.DeleteCliente(idCliente);
            return new { success = true, message = "Cliente removido com sucesso." };
        }
    }
}
