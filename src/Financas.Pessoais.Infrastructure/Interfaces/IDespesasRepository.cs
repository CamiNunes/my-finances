using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;

namespace Financas.Pessoais.Infrastructure.Interfaces
{
    public interface IDespesasRepository
    {
        Task IncluirDespesaAsync(DespesasInputModel receita, string emailUsuario);
        Task<IEnumerable<Despesas>> ObterDespesasAsync(string emailUsuario, int? mes = null, string status = null, string descricao = null);
        Task<IEnumerable<DespesasViewModel>> ObterDespesasPorDescricaoAsync(string descricao, string emailUsuario);
        Task ExcluirDespesaAsync(Guid despesaId, string emailUsuario);
    }
}
