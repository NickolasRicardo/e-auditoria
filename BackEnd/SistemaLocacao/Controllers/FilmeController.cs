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
    public class FilmesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        private readonly FilmeRepositories _filmeRepositories;

        public FilmesController(DatabaseContext context, FilmeRepositories filmeRepositories)
        {
            _filmeRepositories = filmeRepositories;
            _context = context;
        }

        // GET: api/Filmes
        [HttpGet]
        public async Task<ActionResult<List<Filme>>> GetFilmes()
        {
            if (_context.Filme == null)
            {
                return NotFound();
            }
            return await _filmeRepositories.ListFilmes();
        }

        // GET: api/Filmes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Filme>> GetFilme(int id)
        {
            if (_context.Filme == null)
            {
                return NotFound();
            }
            var filme = await _filmeRepositories.GetFilme(id);

            if (filme == null)
            {
                return NotFound();
            }

            return filme;
        }

        // PUT: api/Filmes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilme(int id, RequestModelCreateFilmeDTO model)
        {
            try
            {
                await _filmeRepositories.UpdateFilme(id, model);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST: api/Filmes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Filme>> PostFilme(RequestModelCreateFilmeDTO model)
        {
            try
            {
                var newFilme = await _filmeRepositories.CreateFilme(model);
                return CreatedAtAction("GetFilme", new { id = newFilme.Id }, newFilme);
            }
            catch (Exception ex)
            {
                return BadRequest("Verifique os dados da requisição: " + ex.Message);
            }
        }

        // DELETE: api/Filmes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilme(int id)
        {
            if (_context.Filme == null)
            {
                return NotFound();
            }
            var filme = await _context.Filme.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }

            _context.Filme.Remove(filme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmeExists(int id)
        {
            return (_context.Filme?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
