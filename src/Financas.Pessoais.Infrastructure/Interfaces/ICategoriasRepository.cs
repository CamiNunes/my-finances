using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;

namespace Financas.Pessoais.Infrastructure.Interfaces
{
    public interface ICategoriasRepository
    {
        Task IncluirCategoriaAsync(CategoriaInputModel categoria, string emailUsuario);
        Task<IEnumerable<CategoriaViewModel>> ObterCategoriasAsync(string emailUsuario);
        Task<IEnumerable<CategoriaViewModel>> ObterCategoriasPorDescricaoAsync(string descricao);
        Task ExcluirCategoriaAsync(Guid categoriaId, string emailUsuario);
    }
}
