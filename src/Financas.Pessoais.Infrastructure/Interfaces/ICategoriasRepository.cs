using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;

namespace Financas.Pessoais.Infrastructure.Interfaces
{
    public interface ICategoriasRepository
    {
        Task IncluirCategoriaAsync(CategoriaInputModel categoria);
        Task<IEnumerable<CategoriaViewModel>> ObterCategoriasAsync();
        Task<IEnumerable<CategoriaViewModel>> ObterCategoriasPorDescricaoAsync(string descricao);
        Task ExcluirCategoriaAsync(Guid categoriaId);
    }
}
