using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.FluntContratos;
using Financas.Pessoais.Domain.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financas.Pessoais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> ObterDespesas()
        {
            try
            {
                var result = await _despesasService.ObterDespesasAsync();

                if (result == null || !result.Any())
                {
                    return NotFound("Nenhuma despesa encontrada.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("despesas/{descricao}")]
        public async Task<IActionResult> ObterDespesasPorDescricao(string descricao)
        {
            try
            {
                if (string.IsNullOrEmpty(descricao))
                {
                    return BadRequest("A descrição não pode ser vazia.");
                }

                var result = await _despesasService.ObterDespesasPorDescricaoAsync(descricao);

                if (result == null || !result.Any())
                {
                    return NotFound($"Nenhuma despesa encontrada com a descrição '{descricao}'.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpDelete("despesas/{id}")]
        public async Task<IActionResult> ExcluirDespesa(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("O ID da despesa é inválido.");
                }

                await _despesasService.ExcluirDespesaAsync(id);

                return NoContent(); // Retorna código 204 (No Content) se a exclusão for bem-sucedida
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
