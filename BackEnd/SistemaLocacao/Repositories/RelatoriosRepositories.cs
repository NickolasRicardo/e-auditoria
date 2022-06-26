using Microsoft.EntityFrameworkCore;
using SistemaLocacao.Models;
using SistemaLocacao.Models.RequestModels;
using SistemaLocacao.Models.ViewModels;

namespace SistemaLocacao.Repositories
{
    public class RelatoriosRepositories
    {
        private readonly DatabaseContext _context;
        private readonly ClienteRepositories _clienteRepositories;
        private readonly FilmeRepositories _filmeRepositories;

        private readonly LocacaoRepositories _locacaoRepositories;

        public RelatoriosRepositories(
            DatabaseContext context,
            ClienteRepositories clienteRepositories,
            FilmeRepositories filmeRepositories,
            LocacaoRepositories locacaoRepositories
        )
        {
            _context = context;

            _clienteRepositories = clienteRepositories;
            _filmeRepositories = filmeRepositories;
            _locacaoRepositories = locacaoRepositories;
        }

        public async Task<ViewModelGetClientesAtrasoDTO> ClientesEmAtraso()
        {
            try
            {
                var locacoes = await _context.Locacao
                    .Include(l => l.Cliente)
                    .Include(l => l.Filme)
                    .Where(l => l.DataDevolucao == new DateTime())
                    .ToListAsync();

                ViewModelGetClientesAtrasoDTO vm = new ViewModelGetClientesAtrasoDTO();

                foreach (var locacao in locacoes)
                {
                    if (_locacaoRepositories.VerificaAtraso(locacao.DataLocacao, locacao.Filme))
                    {
                        if (!vm.Clientes.Any(c => c.Id == locacao.Cliente.Id))
                        {
                            ClienteDTO client = new ClienteDTO();

                            client.Id = locacao.Cliente.Id;
                            client.Nome = locacao.Cliente.Nome;

                            vm.Clientes.Add(client);
                        }
                    }
                }

                vm.Quantidade = vm.Clientes.Count;

                return vm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ViewModelGetFilmesNuncaAlugadosDTO> FilmesNaoAlugados()
        {
            try
            {
                var filmes = await _context.Filme
                    .Include(f => f.Locacoes)
                    .Where(f => f.Locacoes.Count == 0)
                    .ToListAsync();

                ViewModelGetFilmesNuncaAlugadosDTO vm = new ViewModelGetFilmesNuncaAlugadosDTO();

                foreach (var item in filmes)
                {
                    FilmeDTO f = new FilmeDTO();

                    f.Id = item.Id;
                    f.Nome = item.Titulo;

                    vm.Filmes.Add(f);
                }

                vm.Quantidade = vm.Filmes.Count;

                return vm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<FilmeDTO>> FilmesMaisAlugados()
        {
            try
            {
                var periodoInicial = new DateTime(DateTime.Now.AddYears(-1).Year, 1, 1);
                var periodoFinal = new DateTime(DateTime.Now.Year, 1, 1);

                var filmes = await _context.Filme
                    .Include(f => f.Locacoes)
                    .Where(
                        f =>
                            f.Locacoes.Any(
                                l =>
                                    l.DataLocacao >= periodoInicial && l.DataLocacao <= periodoFinal
                            )
                    )
                    .OrderBy(f => f.Locacoes.Count())
                    .ToListAsync();

                List<FilmeDTO> vm = new List<FilmeDTO>();

                foreach (var item in filmes)
                {
                    FilmeDTO f = new FilmeDTO();

                    f.Id = item.Id;
                    f.Nome = item.Titulo;

                    vm.Add(f);
                }

                return vm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<FilmeDTO>> FilmesMenosAlugados()
        {
            try
            {
                var periodoInicial = DateTime.Now.AddDays(-7);
                var periodoFinal = DateTime.Now;

                var filmes = await _context.Filme
                    .Include(f => f.Locacoes)
                    .Where(
                        f =>
                            f.Locacoes.Any(
                                l =>
                                    l.DataLocacao >= periodoInicial && l.DataLocacao <= periodoFinal
                            )
                    )
                    .OrderByDescending(f => f.Locacoes.Count())
                    .ToListAsync();

                List<FilmeDTO> vm = new List<FilmeDTO>();

                foreach (var item in filmes)
                {
                    FilmeDTO f = new FilmeDTO();

                    f.Id = item.Id;
                    f.Nome = item.Titulo;

                    vm.Add(f);
                }

                return vm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ClienteDTO> ClienteQueMaisAlugou()
        {
            try
            {
                var clientes = await _context.Cliente
                    .Include(c => c.Locacoes)
                    .Where(c => c.Locacoes.Any())
                    .OrderByDescending(f => f.Locacoes.Count())
                    .ToListAsync();
                ClienteDTO clienteDTO = new ClienteDTO();
                if (clientes.Count >= 2)
                {
                    clienteDTO.Id = clientes[1].Id;
                    clienteDTO.Nome = clientes[1].Nome;
                }
                else
                {
                    clienteDTO.Id = clientes[0].Id;
                    clienteDTO.Nome = clientes[0].Nome;
                }

                return clienteDTO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
