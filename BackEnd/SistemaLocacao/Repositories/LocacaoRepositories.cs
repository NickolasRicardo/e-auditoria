using Microsoft.EntityFrameworkCore;
using SistemaLocacao.Models;
using SistemaLocacao.Models.RequestModels;
using SistemaLocacao.Models.ViewModels;

namespace SistemaLocacao.Repositories
{
    public class LocacaoRepositories
    {
        private readonly DatabaseContext _context;
        private readonly ClienteRepositories _clienteRepositories;
        private readonly FilmeRepositories _filmeRepositories;

        public LocacaoRepositories(
            DatabaseContext context,
            ClienteRepositories clienteRepositories,
            FilmeRepositories filmeRepositories
        )
        {
            _context = context;

            _clienteRepositories = clienteRepositories;
            _filmeRepositories = filmeRepositories;
        }

        public async Task<Locacao> CreateLocacao(RequestModelCreateLocacaoDTO model)
        {
            try
            {
                var cliente = await _clienteRepositories.GetCliente(model.IdCliente);

                if (cliente == null)
                    throw (new Exception("Cliente não encontrado"));

                var filme = await _filmeRepositories.GetFilme(model.IdFilme);

                if (filme == null)
                    throw (new Exception("Filme não encontrado"));

                Locacao newLocacao = new Locacao();

                newLocacao.Cliente = cliente;
                newLocacao.Filme = filme;

                newLocacao.DataLocacao =
                    model.DataLocacao > new DateTime(0001, 01, 01)
                        ? model.DataLocacao
                        : DateTime.Now;

                _context.Locacao.Add(newLocacao);
                await _context.SaveChangesAsync();

                return newLocacao;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Locacao> GetLocacao(int id)
        {
            try
            {
                var locacao = await _context.Locacao.Where(c => c.Id == id).FirstOrDefaultAsync();

                return locacao;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateLocacao(int id, RequestModelUpdateLocacaoDTO model)
        {
            Locacao locacao = await GetLocacao(id);

            if (locacao == null)
            {
                throw (new Exception("Locacao não encontrada"));
            }

            var cliente = await _clienteRepositories.GetCliente(model.IdCliente);

            if (cliente == null)
                throw (new Exception("Cliente não encontrado"));

            var filme = await _filmeRepositories.GetFilme(model.IdFilme);

            if (filme == null)
                throw (new Exception("Filme não encontrado"));

            locacao.Cliente = cliente;
            locacao.Filme = filme;
            locacao.DataLocacao =
                model.DataLocacao > new DateTime(0001, 01, 01)
                    ? model.DataLocacao
                    : locacao.DataLocacao;

            if (model.DataDevolucao > new DateTime(1901, 02, 01))
                locacao.DataDevolucao = model.DataDevolucao;
            else
                locacao.DataDevolucao = new DateTime(0001, 01, 01);

            _context.Entry(locacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (locacao == null)
                {
                    throw (new Exception("Locação não encontrado"));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DevolverFilme(int id, RequestModelDevolverFilmeDTO model)
        {
            Locacao locacao = await GetLocacao(id);

            locacao.DataDevolucao = model.DataDevolucao;

            _context.Entry(locacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (locacao == null)
                {
                    throw (new Exception("Locação não encontrado"));
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<List<ViewModelGetLocacaoDTO>> ListLocacoes()
        {
            try
            {
                List<Locacao> Locacao = await _context.Locacao
                    .Include(l => l.Cliente)
                    .Include(l => l.Filme)
                    .OrderByDescending(l => l.Id)
                    .ToListAsync();

                List<ViewModelGetLocacaoDTO> locacoes = new List<ViewModelGetLocacaoDTO>();

                foreach (var item in Locacao)
                {
                    ViewModelGetLocacaoDTO vm = new ViewModelGetLocacaoDTO();
                    vm.Id = item.Id;
                    vm.ClienteID = item.Cliente.Id;
                    vm.ClienteNome = item.Cliente.Nome;
                    vm.FilmeID = item.Filme.Id;

                    vm.FilmeNome = item.Filme.Titulo;
                    vm.DataDevolucao =
                        item.DataDevolucao > new DateTime(0001, 01, 01)
                            ? item.DataDevolucao.ToString("dd/MM/yyyy")
                            : " - ";
                    vm.DataLocacao = item.DataLocacao.ToString("dd/MM/yyyy");
                    if (item.DataDevolucao == new DateTime(0001, 01, 01, 0, 0, 0, 0))
                    {
                        if (item.Filme.Lancamento && DateTime.Now >= item.DataLocacao.AddDays(2))
                        {
                            vm.StatusDevolucao = "Atrasado";
                        }
                        else if (
                            !item.Filme.Lancamento && DateTime.Now >= item.DataLocacao.AddDays(3)
                        )
                        {
                            vm.StatusDevolucao = "Atrasado";
                        }
                        else
                        {
                            vm.StatusDevolucao = "Em dia";
                        }
                    }
                    else
                    {
                        vm.StatusDevolucao = "Devolvido";
                    }

                    locacoes.Add(vm);
                }

                return locacoes;
            }
            catch
            {
                throw;
            }
        }

        public bool VerificaAtraso(DateTime dataLocacao, Filme filme)
        {
            var dataDevolucao = dataLocacao;

            if (filme.Lancamento)
                dataDevolucao = dataLocacao.AddDays(2);
            else
                dataDevolucao = dataLocacao.AddDays(3);

            if (DateTime.Now < dataDevolucao)
                return false;

            return true;
        }
    }
}
