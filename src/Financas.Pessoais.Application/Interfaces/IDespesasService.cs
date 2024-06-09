using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;

namespace Financas.Pessoais.Application.Interfaces
{
    public interface IDespesasService
    {
        Task IncluirDespesaAsync(DespesasInputModel receita);
        Task<IEnumerable<DespesasViewModel>> ObterDespesasAsync();
        Task<IEnumerable<DespesasViewModel>> ObterDespesasPorDescricaoAsync(string descricao);
        Task ExcluirDespesaAsync(Guid despesaId);
    }
}
