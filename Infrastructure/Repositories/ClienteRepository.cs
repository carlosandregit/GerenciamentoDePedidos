using System.Linq;
using GerenciamentoDePedidosWebApi.Application.Interfaces;
using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Domain.Entities;
using GerenciamentoDePedidosWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDePedidosWebApi.Infrastructure.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(DataContext context)
           : base(context)
        { }

        private DataContext _context
        {
            get { return Context as DataContext; }
        }
        public async Task<List<Cliente>> GetAllAsync()
        {
            await _context.Clientes.FindAsync();
            return _context.Clientes.ToList();
        }

        public async Task<Cliente> GetClienteById(decimal idCliente)
        {
            return await _context.Clientes.FindAsync(idCliente);
        }

        public async Task<Cliente> AdesaoCliente(string nome, string cpf, string email, DateTime dataNascimento, DateTime dtCadastro)
        {
            Cliente cliente = new Cliente();
            cliente.NomeCliente = nome;
            cliente.CPF = cpf;
            cliente.Email = email;
            cliente.DataNascimento = dataNascimento;
            cliente.DataCadastro = dtCadastro;       
         
              _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            return cliente;

        }

        public async Task<dynamic> GetClienteByCpf(string cpf)
        {
            return await _context.Clientes.FindAsync(cpf);
        }

        public async Task<Cliente> AtualizarCliente(decimal idCliente, string nome, string cpf, string email, DateTime dataNascimento, DateTime dtCadastro)
        {
            Cliente cliente = new Cliente();
            cliente.IdCliente = idCliente;
            cliente.NomeCliente = nome;
            cliente.CPF = cpf;
            cliente.Email = email;
            cliente.DataNascimento = dataNascimento;
            cliente.DataCadastro = dtCadastro;

             _context.Clientes.Update(cliente).ReloadAsync();
            await _context.SaveChangesAsync();

            return cliente;

        }
        public async Task<dynamic> DeleteCliente(decimal idCliente)
        {
            var cliente = await _context.Clientes.FindAsync(idCliente);
             _context.Clientes.Remove(cliente);
            return  await _context.SaveChangesAsync();
        }
    }
}
