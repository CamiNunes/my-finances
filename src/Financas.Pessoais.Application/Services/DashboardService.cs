using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.DTOs;
using Financas.Pessoais.Domain.Enums;
using Financas.Pessoais.Domain.Models.ViewModels;
using Financas.Pessoais.Infrastructure.Interfaces;
using Financas.Pessoais.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Financas.Pessoais.Application.Services
{
    public class DashboardService : IDashboardService
    {

        private readonly IDashboardRepository _dashboardRepository;
        private readonly UserContext _userContext;
        private readonly ILogger<DashboardService> _logger;

        public DashboardService(IDashboardRepository dashboardRepository, UserContext userContext, ILogger<DashboardService> logger)
        {
            _dashboardRepository = dashboardRepository;
            _userContext = userContext;
            _logger = logger;
        }

        public List<DespesasDashboardDTO> ListarContasEmAberto(int mes)
        {
            var usuario = _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            var listaContas = _dashboardRepository.ListarContasEmAberto(mes, usuario.Result.Email);
            return listaContas;
        }

        public decimal ObterDiferencaReceitasDespesas(int mes)
        {

            var usuario = _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            var difernca = _dashboardRepository.ObterDiferencaReceitasDespesas(mes, usuario.Result.Email);
            return difernca;
        }

        public int ObterQuantidadeDespesasProximasVencimento(int mes)
        {

            var usuario = _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            var qtdDespesas = _dashboardRepository.ObterQuantidadeDespesasProximasVencimento(mes, usuario.Result.Email);
            return qtdDespesas;
        }

        public decimal ObterSomaDasDespesasDoMes(int mes)
        {

            var usuario =  _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            var soma = _dashboardRepository.ObterSomaDasDespesasDoMes(mes, usuario.Result.Email);
            return soma;
        }

        public decimal ObterSomaDasDespesasEmAbertoDoMes(int mes)
        {

            var usuario = _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            var soma = _dashboardRepository.ObterSomaDasDespesasEmAbertoDoMes(mes, usuario.Result.Email);
            return soma;
        }

        public decimal ObterSomaDasReceitasDoMes(int mes)
        {

            var usuario = _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            var soma = _dashboardRepository.ObterSomaDasReceitasDoMes(mes, usuario.Result.Email);
            return soma;
        }
    }
}
