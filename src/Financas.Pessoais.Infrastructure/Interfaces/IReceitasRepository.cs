using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;

namespace Financas.Pessoais.Infrastructure.Interfaces
{
    public interface IReceitasRepository
    {
        Task IncluirReceitaAsync(ReceitasInputModel receita, string emailUsuario);
        Task<IEnumerable<Receitas>> ObterReceitasAsync(string emailUsuario, int? mes = null, string status = null, string descricao = null);
        Task<IEnumerable<ReceitasViewModel>> ObterReceitasPorDescricaoAsync(string descricao, string emailUsuario);
        Task ExcluirReceitaAsync(Guid receitaId, string emailUsuario);
    }
}
