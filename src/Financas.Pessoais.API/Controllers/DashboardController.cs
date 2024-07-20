using AutoMapper;
using Financas.Pessoais.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financas.Pessoais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly ILogger<DashboardController> _logger;
        private readonly IMapper _mapper;

        public DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger, IMapper mapper)
        {
            _dashboardService = dashboardService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("soma-despesas-mes")]
        public async Task<IActionResult> ObterSomaDasDespesasDoMes(int mes)
        {
            try
            {
                _logger.LogInformation("Endpoint 'SomaDasDespesasDoMes' foi chamado.");

                var result = _dashboardService.ObterSomaDasDespesasDoMes(mes);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar a solicitação.");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("soma-despesas-em-aberto-mes")]
        public async Task<IActionResult> ObterSomaDasDespesasEmAbertoDoMes(int mes)
        {
            try
            {
                _logger.LogInformation("Endpoint 'SomaDasDespesasEmAbertoDoMes' foi chamado.");

                var result = _dashboardService.ObterSomaDasDespesasEmAbertoDoMes(mes);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar a solicitação.");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("soma-receitas-mes")]
        public async Task<IActionResult> ObterSomaDaReceitasDoMes(int mes)
        {
            try
            {
                _logger.LogInformation("Endpoint 'SomaDasReceitasDoMes' foi chamado.");

                var result = _dashboardService.ObterSomaDasReceitasDoMes(mes);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar a solicitação.");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("despesas-a-vencer-mes")]
        public async Task<IActionResult> ObterDespesasAVencerDoMes(int mes)
        {
            try
            {
                _logger.LogInformation("Endpoint 'DespesasAVencerDoMes' foi chamado.");

                var result = _dashboardService.ObterQuantidadeDespesasProximasVencimento(mes);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar a solicitação.");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("saldo-mes")]
        public async Task<IActionResult> ObterDiferencaReceitasDespesas(int mes)
        {
            try
            {
                _logger.LogInformation("Endpoint 'DespesasAVencerDoMes' foi chamado.");

                var result = _dashboardService.ObterDiferencaReceitasDespesas(mes);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar a solicitação.");
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
