﻿using GerenciamentoDePedidosWebApi.Application.Models.Request;
using GerenciamentoDePedidosWebApi.Domain.Entities;

namespace GerenciamentoDePedidosWebApi.Application.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<List<Cliente>> GetAllAsync();
        Task<Cliente> GetClienteById(int idCliente);
        Task<Cliente> AdesaoCliente(string nome,  string cpf, string email,  DateTime dataNascimento, DateTime dtCadastro);
        Task<Cliente> GetClienteByCpf(string cpf);
        Task<Cliente> AtualizarCliente(int idCliente, string nome, string cpf, string email, DateTime dataNascimento, DateTime dtCadastro);
        Task<dynamic> DeleteCliente(int idCliente);
    }
}
