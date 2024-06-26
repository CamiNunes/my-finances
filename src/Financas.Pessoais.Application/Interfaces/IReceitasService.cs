using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;

namespace Financas.Pessoais.Application.Interfaces
{
    public interface IReceitasService
    {
        Task IncluirReceitaAsync(Receitas receita);
        Task<IEnumerable<ReceitasViewModel>> ObterReceitasAsync(int? mes = null, string status = null, string descricao = null);
        Task<IEnumerable<ReceitasViewModel>> ObterReceitasPorDescricaoAsync(string descricao);
        Task ExcluirReceitaAsync(Guid receitaId);
    }
}
    