using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;

namespace Financas.Pessoais.Infrastructure.Interfaces
{
    public interface IReceitasRepository
    {
        Task IncluirReceitaAsync(ReceitasInputModel receita, string emailUsuario);
        Task<IEnumerable<ReceitasViewModel>> ObterReceitasAsync(string emailUsuario);
        Task<IEnumerable<ReceitasViewModel>> ObterReceitasPorDescricaoAsync(string descricao, string emailUsuario);
        Task ExcluirReceitaAsync(Guid receitaId, string emailUsuario);
    }
}
