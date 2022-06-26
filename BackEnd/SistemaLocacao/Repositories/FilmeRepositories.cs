using Microsoft.EntityFrameworkCore;
using SistemaLocacao.Models;
using SistemaLocacao.Models.RequestModels;

namespace SistemaLocacao.Repositories
{
    public class FilmeRepositories
    {
        private readonly DatabaseContext _context;

        public FilmeRepositories(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Filme> CreateFilme(RequestModelCreateFilmeDTO model)
        {
            try
            {
                Filme newFilme = new Filme();

                newFilme.Titulo = model.Titulo;
                newFilme.Lancamento = model.Lancamento;
                newFilme.Classificacao = model.Classificacao;

                _context.Filme.Add(newFilme);
                await _context.SaveChangesAsync();

                return newFilme;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Filme> GetFilme(int id)
        {
            try
            {
                var filme = await _context.Filme.Where(c => c.Id == id).FirstOrDefaultAsync();

                return filme;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateFilme(int id, RequestModelCreateFilmeDTO model)
        {
            Filme filme = await GetFilme(id);

            if (filme == null)
            {
                throw (new Exception("Filme não encontrado"));
            }

            filme.Titulo = model.Titulo;
            filme.Lancamento = model.Lancamento;
            filme.Classificacao = model.Classificacao;

            _context.Entry(filme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (filme == null)
                {
                    throw (new Exception("Filme não encontrado"));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<List<Filme>> ListFilmes()
        {
            try
            {
                List<Filme> filmes = await _context.Filme
                    .OrderByDescending(f => f.Id)
                    .ToListAsync();

                return filmes;
            }
            catch
            {
                throw;
            }
        }
    }
}
