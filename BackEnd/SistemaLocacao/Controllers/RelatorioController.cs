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
    public class RelatorioController : ControllerBase
    {
        private readonly DatabaseContext _context;

        private readonly RelatoriosRepositories _relatoriosRepositories;

        public RelatorioController(
            DatabaseContext context,
            RelatoriosRepositories relatoriosRepositories
        )
        {
            _context = context;
            _relatoriosRepositories = relatoriosRepositories;
        }

        // GET: api/Locacaos
        [HttpGet("ClientesEmAtraso")]
        public async Task<ActionResult<ViewModelGetClientesAtrasoDTO>> GetClientesEmAtraso()
        {
            try
            {
                var vm = await _relatoriosRepositories.ClientesEmAtraso();

                return Ok(vm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("FilmesNaoAlugados")]
        public async Task<ActionResult<ViewModelGetFilmesNuncaAlugadosDTO>> GetFilmesNaoAlugados()
        {
            try
            {
                var vm = await _relatoriosRepositories.FilmesNaoAlugados();

                return Ok(vm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("FilmesMaisAlugados")]
        public async Task<ActionResult<List<FilmeDTO>>> FilmesMaisAlugados()
        {
            try
            {
                var vm = await _relatoriosRepositories.FilmesMaisAlugados();

                return Ok(vm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("FilmesMenosAlugados")]
        public async Task<ActionResult<List<FilmeDTO>>> FilmesMenosAlugados()
        {
            try
            {
                var vm = await _relatoriosRepositories.FilmesMenosAlugados();

                return Ok(vm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("ClienteQueMaisAlugou")]
        public async Task<ActionResult<ClienteDTO>> ClienteQueMaisAlugou()
        {
            try
            {
                var vm = await _relatoriosRepositories.ClienteQueMaisAlugou();

                return Ok(vm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
