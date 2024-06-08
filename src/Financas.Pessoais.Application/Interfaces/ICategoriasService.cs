using Financas.Pessoais.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Application.Interfaces
{
    public interface ICategoriasService
    {
        Task IncluirCategoriaAsync(Categoria categoria);
        Task<IEnumerable<Categoria>> ObterCategoriasAsync();
        Task<IEnumerable<Categoria>> ObterCategoriasPorDescricaoAsync(string descricao);
        Task ExcluirCategoriaAsync(Guid categoriaId);
    }
}
