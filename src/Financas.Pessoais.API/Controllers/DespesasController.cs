using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Application.Services;
using Financas.Pessoais.Domain.Models.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace Financas.Pessoais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasController : ControllerBase
    {
        private readonly IDespesasService _despesasService;

        public DespesasController(IDespesasService despesasService)
        {
            _despesasService = despesasService;
        }

        [HttpPost("despesa")]
        public async Task<IActionResult> IncluirDespesa(DespesasInputModel despesa)
        {
            await _despesasService.IncluirDespesaAsync(despesa);
            return Ok("Despesa registrada com sucesso.");
        }

        [HttpGet("listar-despesas")]
        public async Task<IActionResult> ObterReceitas()
        {
            var result = await _despesasService.ObterDespesasAsync();
            return Ok(result);
        }

        [HttpGet("despesas/{descricao}")]
        public async Task<IActionResult> ObterReceitasPorDescricao(string descricao)
        {
            var result = await _despesasService.ObterDespesasPorDescricaoAsync(descricao);
            return Ok(result);
        }
    }
}
