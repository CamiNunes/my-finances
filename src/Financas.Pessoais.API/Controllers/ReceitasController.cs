using AutoMapper;
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
        private readonly ILogger<ReceitasController> _logger;
        private readonly IMapper _mapper;

        public ReceitasController(IReceitasService receitasService, ILogger<ReceitasController> logger, IMapper mapper)
        {
            _receitasService = receitasService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("receita")]
        public async Task<IActionResult> IncluirReceita([FromBody] ReceitasInputModel receitasInputModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Modelo de entrada inválido para inclusão de receita: {@ModelState}", ModelState);
                    return BadRequest(ModelState);
                }

                var contract = new ReceitasInputModelContrato(receitasInputModel);
                if (!contract.IsValid)
                {
                    _logger.LogWarning("Contrato de validação falhou para inclusão de receita: {@Notifications}", contract.Notifications);
                    return BadRequest(contract.Notifications);
                }

                var novaReceita = _mapper.Map<Receitas>(receitasInputModel);

                await _receitasService.IncluirReceitaAsync(novaReceita);
                
                _logger.LogInformation("Receita incluída com sucesso: {@Receita}", novaReceita);
                return Ok(novaReceita);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao incluir receita: {Message}", ex.Message);
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("listar-receitas")]
        public async Task<IActionResult> ObterReceitas([FromQuery] int? mes = null, [FromQuery] string status = null, [FromQuery] string descricao = null)
        {
            try
            {
                _logger.LogInformation("Iniciando a obtenção de receitas.");

                var result = await _receitasService.ObterReceitasAsync(mes, status, descricao);

                if (result == null || !result.Any())
                {
                    _logger.LogWarning("Nenhuma receita encontrada.");
                    return NotFound("Nenhuma receita encontrada.");
                }

                _logger.LogInformation("Receitas obtidas com sucesso: {@Receitas}", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter receitas: {Message}", ex.Message);
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("receitas/{descricao}")]
        public async Task<IActionResult> ObterReceitasPorDescricao(string descricao)
        {
            try
            {
                _logger.LogInformation("Iniciando a obtenção de receitas com a descrição: {Descricao}", descricao);

                if (string.IsNullOrWhiteSpace(descricao))
                {
                    _logger.LogWarning("A descrição fornecida está vazia.");
                    return BadRequest("A descrição não pode ser vazia.");
                }

                if (descricao.Length > 50)
                {
                    _logger.LogWarning("A descrição fornecida excede 50 caracteres.");
                    return BadRequest("A descrição não pode exceder 50 caracteres.");
                }

                var result = await _receitasService.ObterReceitasPorDescricaoAsync(descricao);

                if (result == null || !result.Any())
                {
                    _logger.LogInformation("Nenhuma receita encontrada com a descrição fornecida: {Descricao}", descricao);
                    return NotFound("Nenhuma receita encontrada com a descrição fornecida.");
                }

                _logger.LogInformation("Receitas obtidas com sucesso para a descrição: {Descricao}", descricao);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter receitas com a descrição: {Descricao}", descricao);
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpDelete("receitas/{id}")]
        public async Task<IActionResult> ExcluirReceita(Guid id)
        {
            try
            {
                _logger.LogInformation("Iniciando a exclusão da receita com ID: {Id}", id);

                if (id == Guid.Empty)
                {
                    _logger.LogWarning("O ID da receita fornecido é inválido.");
                    return BadRequest("O ID da receita é inválido.");
                }

                await _receitasService.ExcluirReceitaAsync(id);

                _logger.LogInformation("Receita com ID: {Id} excluída com sucesso.", id);

                return NoContent(); // Retorna código 204 (No Content) se a exclusão for bem-sucedida
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir receita com ID: {Id}", id);
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
