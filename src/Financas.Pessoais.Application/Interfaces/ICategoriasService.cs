using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;

namespace Financas.Pessoais.Application.Interfaces
{
    public interface ICategoriasService
    {
        Task IncluirCategoriaAsync(Categoria categoria);
        Task<IEnumerable<CategoriaViewModel>> ObterCategoriasAsync();
        Task<IEnumerable<CategoriaViewModel>> ObterCategoriasPorDescricaoAsync(string descricao);
        Task ExcluirCategoriaAsync(Guid categoriaId);
    }
}
