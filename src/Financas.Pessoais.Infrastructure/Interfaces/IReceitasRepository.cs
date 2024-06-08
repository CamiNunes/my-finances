using Financas.Pessoais.Domain.Entidades;

namespace Financas.Pessoais.Infrastructure.Interfaces
{
    public interface IReceitasRepository
    {
        Task IncluirReceitaAsync(Receitas receita);
        Task<IEnumerable<Receitas>> ObterReceitasAsync();
        Task<IEnumerable<Receitas>> ObterReceitasPorDescricaoAsync(string descricao);
    }
}
