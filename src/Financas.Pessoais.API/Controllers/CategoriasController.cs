using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.FluntContracts;
using Financas.Pessoais.Domain.Models.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace Financas.Pessoais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasService _categoriasService;
        

        public CategoriasController(ICategoriasService categoriasService)
        {
            _categoriasService = categoriasService;
            
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
            var result = await _categoriasService.ObterCategoriasAsync();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirCategoria(Guid id)
        {
            try
            {
                await _categoriasService.ExcluirCategoriaAsync(id);
                return NoContent(); // Retorna código 204 (No Content) se a exclusão for bem-sucedida
            }
            catch (Exception ex)
            {
                // Aqui você pode lidar com exceções, como retornar um código de erro 500 (Internal Server Error)
                return StatusCode(500, $"Erro ao excluir categoria: {ex.Message}");
            }
        }
    }
}
