using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> IncluirDespesa(Despesas despesas)
        {
            await _despesasService.IncluirDespesaAsync(despesas);
            return Ok("Despesa registrada com sucesso.");
        }
    }
}
