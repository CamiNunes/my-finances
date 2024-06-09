using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.FluntContratos;
using Financas.Pessoais.Domain.Models.InputModels;
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
        public async Task<IActionResult> IncluirReceita([FromBody] ReceitasInputModel receitasInputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contract = new ReceitasInputModelContrato(receitasInputModel);
            if (!contract.IsValid)
            {
                return BadRequest(contract.Notifications);
            }

            // Mapeie o modelo de entrada para a entidade de domínio, se necessário
            var receita = new ReceitasInputModel // Supondo que Receita seja sua entidade de domínio
            {
                Recebido = receitasInputModel.Recebido,
                DataRecebimento = receitasInputModel.DataRecebimento ?? DateTime.MinValue, // Use MinValue ou uma lógica padrão
                TipoReceita = receitasInputModel.TipoReceita,
                Valor = receitasInputModel.Valor,
                Descricao = receitasInputModel.Descricao,
                Categoria = receitasInputModel.Categoria
            };

            await _receitasService.IncluirReceitaAsync(receita);
            return Ok(receita);
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
