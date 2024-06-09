using Financas.Pessoais.Application.Interfaces;
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

        public DespesasService(IDespesasRepository despesasRepository)
        {
            _despesasRepository = despesasRepository;
        }

        public async Task IncluirDespesaAsync(DespesasInputModel despesa)
        {
            //bool pago = despesa.Pago = true;

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

            await _despesasRepository.IncluirDespesaAsync(novaDespesa);
        }

        public async Task<IEnumerable<DespesasViewModel>> ObterDespesasAsync()
        {
            return await _despesasRepository.ObterDespesasAsync();
        }

        public async Task<IEnumerable<DespesasViewModel>> ObterDespesasPorDescricaoAsync(string descricao)
        {
            return await _despesasRepository.ObterDespesasPorDescricaoAsync(descricao);
        }

        public async Task ExcluirDespesaAsync(Guid despesaId)
        {
            await _despesasRepository.ExcluirDespesaAsync(despesaId);
        }
    }
}
