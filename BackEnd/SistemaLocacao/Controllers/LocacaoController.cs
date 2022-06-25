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
using SistemaLocacao.Models.ViewModels;
using SistemaLocacao.Repositories;

namespace SistemaLocacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaosController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly LocacaoRepositories _locacaoRepositories;

        public LocacaosController(DatabaseContext context, LocacaoRepositories locacaoRepositories)
        {
            _context = context;
            _locacaoRepositories = locacaoRepositories;
        }

        // GET: api/Locacaos
        [HttpGet]
        public async Task<ActionResult<List<ViewModelGetLocacaoDTO>>> GetLocacao()
        {
            if (_context.Locacao == null)
            {
                return NotFound();
            }
            return await _locacaoRepositories.ListLocacoes();
        }

        // GET: api/Locacaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Locacao>> GetLocacao(int id)
        {
            if (_context.Locacao == null)
            {
                return NotFound();
            }
            var locacao = await _context.Locacao.FindAsync(id);

            if (locacao == null)
            {
                return NotFound();
            }

            return locacao;
        }

        // PUT: api/Locacaos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocacao(int id, Locacao locacao)
        {
            if (id != locacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(locacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocacaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Locacaos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Locacao>> PostLocacao(RequestModelCreateLocacaoDTO model)
        {
            try
            {
                var newFilme = await _locacaoRepositories.CreateLocacao(model);
                return CreatedAtAction("GetLocacao", new { id = newFilme.Id }, newFilme);
            }
            catch (Exception ex)
            {
                return BadRequest("Verifique os dados da requisição: " + ex.Message);
            }
        }

        // DELETE: api/Locacaos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocacao(int id)
        {
            if (_context.Locacao == null)
            {
                return NotFound();
            }
            var locacao = await _context.Locacao.FindAsync(id);
            if (locacao == null)
            {
                return NotFound();
            }

            _context.Locacao.Remove(locacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocacaoExists(int id)
        {
            return (_context.Locacao?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
