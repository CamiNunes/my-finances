using Financas.Pessoais.Application.Interfaces;
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

        [HttpPost]
        public async Task<IActionResult> IncluirDespesa(DespesasInputModel despesas)
        {
            await _despesasService.IncluirDespesaAsync(despesas);
            return Ok("Despesa registrada com sucesso.");
        }
    }
}
