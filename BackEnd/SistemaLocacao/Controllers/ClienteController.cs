using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaLocacao;
using SistemaLocacao.Models;
using SistemaLocacao.Models.RequestModels;
using SistemaLocacao.Repositories;

namespace SistemaLocacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly ClienteRepositories _clienteRepositories;

        public ClientesController(DatabaseContext context, ClienteRepositories clienteRepositories)
        {
            _clienteRepositories = clienteRepositories;
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetClientes()
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            return await _clienteRepositories.ListClientes();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClienteByID(int id)
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            var cliente = await _clienteRepositories.GetCliente(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(
            int id,
            [FromBody] RequestModelCreateClienteDTO model
        )
        {
            try
            {
                await _clienteRepositories.UpdateCliente(id, model);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateClient(RequestModelCreateClienteDTO model)
        {
            try
            {
                var newCliente = await _clienteRepositories.CreateCliente(model);
                return CreatedAtAction("GetCliente", new { id = newCliente.Id }, newCliente);
            }
            catch (Exception ex)
            {
                return BadRequest("Verifique os dados da requisição: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
