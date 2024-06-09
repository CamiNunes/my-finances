using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Application.Services;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.FluntContratos;
using Financas.Pessoais.Domain.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financas.Pessoais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            try
            {
                var result = await _receitasService.ObterReceitasAsync();

                if (result == null || !result.Any())
                {
                    return NotFound("Nenhuma receita encontrada.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("receitas/{descricao}")]
        public async Task<IActionResult> ObterReceitasPorDescricao(string descricao)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(descricao))
                {
                    return BadRequest("A descrição não pode ser vazia.");
                }

                if (descricao.Length > 50)
                {
                    return BadRequest("A descrição não pode exceder 50 caracteres.");
                }

                var result = await _receitasService.ObterReceitasPorDescricaoAsync(descricao);

                if (result == null || !result.Any())
                {
                    return NotFound("Nenhuma receita encontrada com a descrição fornecida.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpDelete("receitas/{id}")]
        public async Task<IActionResult> ExcluirDespesa(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("O ID da receita é inválido.");
                }

                await _receitasService.ExcluirReceitaAsync(id);

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
