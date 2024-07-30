using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;

namespace Financas.Pessoais.Infrastructure.Interfaces
{
    public interface IDespesasRepository
    {
        Task IncluirDespesaAsync(DespesasInputModel receita, string emailUsuario);
        Task AlterarDespesaAsync(DespesasUpdateModel despesa, string emailUsuario);
        Task<PagedResult<Despesas>> ObterDespesasAsync(string emailUsuario, int? mes = null, string status = null, string descricao = null, PaginationParameters paginationParameters = null);
        Task<IEnumerable<DespesasViewModel>> ObterDespesasPorDescricaoAsync(string descricao, string emailUsuario);
        Task ExcluirDespesaAsync(Guid despesaId, string emailUsuario);
    }
}
