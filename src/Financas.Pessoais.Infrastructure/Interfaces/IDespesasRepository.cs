using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;

namespace Financas.Pessoais.Infrastructure.Interfaces
{
    public interface IDespesasRepository
    {
        Task IncluirDespesaAsync(DespesasInputModel receita);
        Task<IEnumerable<DespesasViewModel>> ObterDespesasAsync();
        Task<IEnumerable<DespesasViewModel>> ObterDespesasPorDescricaoAsync(string descricao);
    }
}
