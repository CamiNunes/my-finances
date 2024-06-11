using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;

namespace Financas.Pessoais.Infrastructure.Interfaces
{
    public interface IDespesasRepository
    {
        Task IncluirDespesaAsync(DespesasInputModel receita, string emailUsuario);
        Task<IEnumerable<DespesasViewModel>> ObterDespesasAsync(string emailUsuario);
        Task<IEnumerable<DespesasViewModel>> ObterDespesasPorDescricaoAsync(string descricao, string emailUsuario);
        Task ExcluirDespesaAsync(Guid despesaId, string emailUsuario);
    }
}
