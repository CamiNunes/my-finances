using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Enums;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;
using Financas.Pessoais.Infrastructure.Interfaces;

namespace Financas.Pessoais.Application.Services
{
    public class DespesasService : IDespesasService
    {
        private readonly IDespesasRepository _despesasRepository;
        private readonly AuthService _authService;
        private readonly UserContext _userContext;

        public DespesasService(IDespesasRepository despesasRepository, AuthService authService, UserContext userContext)
        {
            _despesasRepository = despesasRepository;
            _authService = authService;
            _userContext = userContext;
        }

        public async Task IncluirDespesaAsync(Despesas despesa)
        {
            var usuario = await _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            var novaDespesa = new DespesasInputModel
            {
                Descricao = despesa.Descricao,
                TipoDespesa = despesa.TipoDespesa == TipoDespesaEnum.Pessoal ? TipoDespesaEnum.Pessoal : TipoDespesaEnum.Casa,
                Valor = despesa.Valor,
                Pago = despesa.Pago,
                DataVencimento = despesa.DataVencimento,
                DataPagamento = despesa.DataPagamento,
                Categoria = despesa.Categoria
            };

            await _despesasRepository.IncluirDespesaAsync(novaDespesa, usuario.Email);
        }

        public async Task<IEnumerable<DespesasViewModel>> ObterDespesasAsync(int? mes = null, string status = null, string descricao = null)
        {
            var usuario = await _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            var despesas = await _despesasRepository.ObterDespesasAsync(usuario.Email, mes, status, descricao);

            var despesasViewModel = despesas.Select(despesa => new DespesasViewModel
            {
                Id = despesa.Id,
                Valor = despesa.Valor,
                Descricao = despesa.Descricao,
                Pago = despesa.Pago,
                DataVencimento = despesa.DataVencimento,
                DataPagamento = despesa.DataPagamento,
                TipoDespesa = despesa.TipoDespesa,
                Categoria = despesa.Categoria,
                StatusDespesa = despesa.Pago ? "PAGO"
                                : despesa.DataVencimento <= DateTime.UtcNow ? "VENCIDO"
                                : "ABERTO"
            }).ToList();

            return despesasViewModel;
        }

        public async Task<IEnumerable<DespesasViewModel>> ObterDespesasAsync()
        {
            var usuario = await _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            var despesas = await _despesasRepository.ObterDespesasAsync(usuario.Email);

            var despesasViewModel = despesas.Select(despesa => new DespesasViewModel
            {
                Id = despesa.Id,
                Valor = despesa.Valor,
                Descricao = despesa.Descricao,
                Pago = despesa.Pago,
                DataVencimento = despesa.DataVencimento,
                DataPagamento = despesa.DataPagamento,
                TipoDespesa = despesa.TipoDespesa,
                Categoria = despesa.Categoria,
                StatusDespesa = despesa.Pago ? "PAGO"
                                : despesa.DataVencimento <= DateTime.UtcNow ? "VENCIDO"
                                : "ABERTO"
            }).ToList();

            return despesasViewModel;
        }

        public async Task<IEnumerable<DespesasViewModel>> ObterDespesasPorDescricaoAsync(string descricao)
        {
            var usuario = await _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            return await _despesasRepository.ObterDespesasPorDescricaoAsync(descricao, usuario.Email);
        }

        public async Task ExcluirDespesaAsync(Guid despesaId)
        {
            var usuario = await _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            await _despesasRepository.ExcluirDespesaAsync(despesaId, usuario.Email);
        }
    }
}
