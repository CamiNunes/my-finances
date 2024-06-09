using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.FluntContracts;
using Financas.Pessoais.Domain.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Financas.Pessoais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasService _categoriasService;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(ICategoriasService categoriasService, ILogger<CategoriasController> logger)
        {
            _categoriasService = categoriasService;
            _logger = logger;
        }

        [HttpPost("categoria")]
        public async Task<IActionResult> IncluirCategoria(CategoriaInputModel categoria)
        {
            var contract = new CategoriaInputModelContrato(categoria);
            if (!contract.IsValid)
            {
                return BadRequest(contract.Notifications);
            }

            await _categoriasService.IncluirCategoriaAsync(categoria);
            return Ok(categoria);
        }

        [HttpGet("listar-categorias")]
        public async Task<IActionResult> ObterCategorias()
        {
            try
            {
                _logger.LogInformation("Endpoint 'ObterCategorias' foi chamado.");

                var result = await _categoriasService.ObterCategoriasAsync();

                if (result == null || !result.Any())
                {
                    _logger.LogWarning("Nenhuma categoria encontrada.");
                    return NotFound("Nenhuma categoria encontrada.");
                }

                _logger.LogInformation("Categorias encontradas: {@categorias}", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar a solicitação.");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirCategoria(Guid id)
        {
            try
            {
                await _categoriasService.ExcluirCategoriaAsync(id);
                _logger.LogInformation($"Categoria com ID {id} excluída com sucesso.");
                return NoContent(); // Retorna código 204 (No Content) se a exclusão for bem-sucedida
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir categoria com ID {id}: {ex.Message}");
                // Aqui você pode lidar com exceções, como retornar um código de erro 500 (Internal Server Error)
                return StatusCode(500, $"Erro ao excluir categoria: {ex.Message}");
            }
        }
    }
}
