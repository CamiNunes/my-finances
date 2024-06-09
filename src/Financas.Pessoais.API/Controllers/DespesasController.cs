using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.FluntContratos;
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
        public async Task<IActionResult> IncluirDespesa(DespesasInputModel despesasInputModel)
        {
            var contract = new DespesasInputModelContrato(despesasInputModel);
            if (!contract.IsValid)
            {
                return BadRequest(contract.Notifications);
            }

            // Mapeie o modelo de entrada para a entidade de domínio, se necessário
            var despesa = new DespesasInputModel
            {
                Valor = despesasInputModel.Valor,
                Descricao = despesasInputModel.Descricao,
                Categoria = despesasInputModel.Categoria,
                Pago = despesasInputModel.Pago,
                DataVencimento = despesasInputModel.DataVencimento,
                DataPagamento = despesasInputModel.DataPagamento,
                TipoDespesa = despesasInputModel.TipoDespesa
            };

            await _despesasService.IncluirDespesaAsync(despesa);
            return Ok(despesa);
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
