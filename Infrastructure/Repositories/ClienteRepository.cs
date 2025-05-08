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
           return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetClienteById(int idCliente)
        {
            return await _context.Clientes.FindAsync(idCliente);
        }

        public async Task<Cliente> AdesaoCliente(string nome, string cpf, string email, DateTime dataNascimento, DateTime dtCadastro)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.NomeCliente = nome;
                cliente.CPF = cpf;
                cliente.Email = email;
                cliente.DataNascimento = dataNascimento;
                cliente.DataCadastro = dtCadastro;

                await _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<Cliente> GetClienteByCpf(string cpf)
        {
            return await _context.Clientes.FirstOrDefaultAsync(x => x.CPF == cpf);
        }

        public async Task<Cliente> AtualizarCliente(int idCliente, string nome, string cpf, string email, DateTime dataNascimento, DateTime dtCadastro)
        {
            var clienteExistente = await _context.Clientes.FindAsync(idCliente);
            clienteExistente.NomeCliente = nome;
            clienteExistente.CPF = cpf;
            clienteExistente.Email = email;
            clienteExistente.DataNascimento = dataNascimento;
            clienteExistente.DataCadastro = dtCadastro;

            await _context.SaveChangesAsync();
            return clienteExistente;

        }
        public async Task<dynamic> DeleteCliente(int idCliente)
        {
            var cliente = await _context.Clientes.FindAsync(idCliente);
             _context.Clientes.Remove(cliente);
            return  await _context.SaveChangesAsync();
        }
    }
}
