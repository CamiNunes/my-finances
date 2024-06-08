using Financas.Pessoais.Domain.Entidades;

namespace Financas.Pessoais.Infrastructure.Interfaces
{
    public interface ICategoriasRepository
    {
        Task IncluirCategoriaAsync(Categoria categoria);
        Task<IEnumerable<Categoria>> ObterCategoriasAsync();
        Task<IEnumerable<Categoria>> ObterCategoriasPorDescricaoAsync(string descricao);
        Task ExcluirCategoriaAsync(Guid categoriaId);
    }
}
