using Microsoft.EntityFrameworkCore;
using SistemaLocacao.Models;
using SistemaLocacao.Models.RequestModels;

namespace SistemaLocacao.Repositories
{
    public class ClienteRepositories
    {
        private readonly DatabaseContext _context;

        public ClienteRepositories(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Cliente> CreateCliente(RequestModelCreateClienteDTO model)
        {
            try
            {
                Cliente newCliente = new Cliente();

                newCliente.Nome = model.Nome;
                newCliente.CPF = model.Cpf;

                newCliente.DataNascimento = model.DataNascimento;

                _context.Add(newCliente);
                await _context.SaveChangesAsync();

                return newCliente;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Cliente> GetCliente(int id)
        {
            try
            {
                var cliente = await _context.Cliente.Where(c => c.Id == id).FirstOrDefaultAsync();

                return cliente;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateCliente(int id, RequestModelCreateClienteDTO model)
        {
            Cliente cliente = await GetCliente(id);

            if (cliente == null)
            {
                throw (new Exception("Cliente não encontrado"));
            }

            cliente.Nome = model.Nome;
            cliente.CPF = model.Cpf;
            cliente.DataNascimento = model.DataNascimento;

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (cliente == null)
                {
                    throw (new Exception("Cliente não encontrado"));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<List<Cliente>> ListClientes()
        {
            try
            {
                List<Cliente> clientes = await _context.Cliente.ToListAsync();

                return clientes;
            }
            catch
            {
                throw;
            }
        }
    }
}
