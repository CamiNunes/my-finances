using AutoMapper;
using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.Entidades;
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
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriasService categoriasService, ILogger<CategoriasController> logger, IMapper mapper)
        {
            _categoriasService = categoriasService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("categoria")]
        public async Task<IActionResult> IncluirCategoria(CategoriaInputModel categoriaInputModel)
        {
            try
            {
                _logger.LogInformation("Iniciando a inclusão de uma nova categoria.");

                var contract = new CategoriaInputModelContrato(categoriaInputModel);
                if (!contract.IsValid)
                {
                    _logger.LogWarning("Dados de entrada inválidos para a categoria: {Erros}", contract.Notifications);
                    return BadRequest(contract.Notifications);
                }

                var novaCategoria = _mapper.Map<Categoria>(categoriaInputModel);
                
                await _categoriasService.IncluirCategoriaAsync(novaCategoria);

                _logger.LogInformation("Categoria incluída com sucesso: {Categoria}", novaCategoria);
                return Ok(novaCategoria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao incluir nova categoria.");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
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
