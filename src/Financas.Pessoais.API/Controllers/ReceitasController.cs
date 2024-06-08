using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Financas.Pessoais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitasController : ControllerBase
    {
        private readonly IReceitasService _receitasService;

        public ReceitasController(IReceitasService receitasService)
        {
            _receitasService = receitasService;
        }

        [HttpPost("receita")]
        public async Task<IActionResult> IncluirReceita(Receitas receitas)
        {
            await _receitasService.IncluirReceitaAsync(receitas);
            return Ok("Receita registrada com sucesso.");
        }

        [HttpGet("listar-receitas")]
        public async Task<IActionResult> ObterReceitas()
        {
            var result = await _receitasService.ObterReceitasAsync();
            return Ok(result);
        }

        [HttpGet("receitas/{descricao}")]
        public async Task<IActionResult> ObterReceitasPorDescricao(string descricao)
        {
            var result = await _receitasService.ObterReceitasPorDescricaoAsync(descricao);
            return Ok(result);
        }
    }
}
