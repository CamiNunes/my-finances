using AutoMapper;
using Financas.Pessoais.Application.Interfaces;
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
    public class DespesasController : ControllerBase
    {
        private readonly IDespesasService _despesasService;
        private readonly ILogger<DespesasController> _logger;
        private readonly IMapper _mapper;

        public DespesasController(IDespesasService despesasService, 
                                  ILogger<DespesasController> logger, 
                                  IMapper mapper)
        {
            _despesasService = despesasService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("despesa")]
        public async Task<IActionResult> IncluirDespesa(DespesasInputModel despesasInputModel)
        {
            try
            {
                _logger.LogInformation("Iniciando a inclusão de uma nova despesa.");

                var contract = new DespesasInputModelContrato(despesasInputModel);
                if (!contract.IsValid)
                {
                    _logger.LogWarning("Dados de entrada inválidos para a despesa: {Erros}", contract.Notifications);
                    return BadRequest(contract.Notifications);
                }

                var novaDespesa = _mapper.Map<Despesas>(despesasInputModel);
                
                await _despesasService.IncluirDespesaAsync(novaDespesa);

                _logger.LogInformation("Despesa incluída com sucesso: {Despesa}", novaDespesa);
                return Ok(novaDespesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao incluir nova despesa.");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("listar-despesas")]
        public async Task<IActionResult> ObterDespesas()
        {
            try
            {
                var result = await _despesasService.ObterDespesasAsync();

                if (result == null || !result.Any())
                {
                    _logger.LogWarning("Nenhuma despesa encontrada.");
                    return NotFound("Nenhuma despesa encontrada.");
                }

                _logger.LogInformation("Despesas obtidas com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro interno do servidor: {ex.Message}");
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
                    _logger.LogWarning("A descrição da despesa não pode ser vazia.");
                    return BadRequest("A descrição não pode ser vazia.");
                }

                var result = await _despesasService.ObterDespesasPorDescricaoAsync(descricao);

                if (result == null || !result.Any())
                {
                    _logger.LogInformation($"Nenhuma despesa encontrada com a descrição '{descricao}'.");
                    return NotFound($"Nenhuma despesa encontrada com a descrição '{descricao}'.");
                }

                _logger.LogInformation($"Despesas encontradas com a descrição '{descricao}'.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro interno do servidor ao obter despesas com a descrição '{descricao}': {ex.Message}");
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
                    _logger.LogWarning("Tentativa de exclusão com ID de despesa inválido.");
                    return BadRequest("O ID da despesa é inválido.");
                }

                await _despesasService.ExcluirDespesaAsync(id);
                _logger.LogInformation($"Despesa com ID {id} excluída com sucesso.");
                return NoContent(); // Retorna código 204 (No Content) se a exclusão for bem-sucedida
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir a despesa com ID {id}: {ex.Message}");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
