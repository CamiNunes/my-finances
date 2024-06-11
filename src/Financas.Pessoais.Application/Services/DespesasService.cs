using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Enums;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;
using Financas.Pessoais.Infrastructure.Interfaces;
using Financas.Pessoais.Infrastructure.Repositories;

namespace Financas.Pessoais.Application.Services
{
    public class DespesasService : IDespesasService
    {
        private readonly IDespesasRepository _despesasRepository;
        private readonly AuthService _authService;

        public DespesasService(IDespesasRepository despesasRepository, AuthService authService)
        {
            _despesasRepository = despesasRepository;
            _authService = authService;
        }

        private async Task VerificarSeUsuarioEhAdministradorAsync()
        {
            var usuario = await _authService.ObterUsuarioAutenticadoAsync();
            if (usuario == null || !usuario.IsAdmin)
            {
                throw new UnauthorizedAccessException("Usuário não tem permissão para realizar esta ação.");
            }
        }

        public async Task IncluirDespesaAsync(DespesasInputModel despesa)
        {
            await VerificarSeUsuarioEhAdministradorAsync();
            var usuario = await _authService.ObterUsuarioAutenticadoAsync();
           
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

        public async Task<IEnumerable<DespesasViewModel>> ObterDespesasAsync()
        {
            await VerificarSeUsuarioEhAdministradorAsync();
            var usuario = await _authService.ObterUsuarioAutenticadoAsync();

            return await _despesasRepository.ObterDespesasAsync(usuario.Email);
        }

        public async Task<IEnumerable<DespesasViewModel>> ObterDespesasPorDescricaoAsync(string descricao)
        {
            await VerificarSeUsuarioEhAdministradorAsync();
            var usuario = await _authService.ObterUsuarioAutenticadoAsync();

            return await _despesasRepository.ObterDespesasPorDescricaoAsync(descricao, usuario.Email);
        }

        public async Task ExcluirDespesaAsync(Guid despesaId)
        {
            await VerificarSeUsuarioEhAdministradorAsync();
            var usuario = await _authService.ObterUsuarioAutenticadoAsync();

            await _despesasRepository.ExcluirDespesaAsync(despesaId, usuario.Email);
        }
    }
}
