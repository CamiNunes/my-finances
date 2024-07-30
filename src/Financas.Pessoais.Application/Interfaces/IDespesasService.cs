using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;

namespace Financas.Pessoais.Application.Interfaces
{
    public interface IDespesasService
    {
        Task IncluirDespesaAsync(Despesas receita);
        Task AlterarDespesaAsync(DespesasUpdateModel despesa);
        Task<PagedResult<DespesasViewModel>> ObterDespesasAsync(int? mes, string status, string descricao, PaginationParameters paginationParameters);
        Task<IEnumerable<DespesasViewModel>> ObterDespesasPorDescricaoAsync(string descricao);
        Task ExcluirDespesaAsync(Guid despesaId);
    }
}
