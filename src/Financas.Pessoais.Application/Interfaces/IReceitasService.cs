using Financas.Pessoais.Domain.Entidades;

namespace Financas.Pessoais.Application.Interfaces
{
    public interface IReceitasService
    {
        Task IncluirReceitaAsync(Receitas receita);
        Task<IEnumerable<Receitas>> ObterReceitasAsync();
        Task<IEnumerable<Receitas>> ObterReceitasPorDescricaoAsync(string descricao);
    }
}
