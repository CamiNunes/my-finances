using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;
using Financas.Pessoais.Infrastructure.Interfaces;

namespace Financas.Pessoais.Application.Services
{
    public class CategoriasService : ICategoriasService
    {
        private readonly ICategoriasRepository _categoriasRepository;

        public CategoriasService(ICategoriasRepository categoriasRepository)
        {
            _categoriasRepository = categoriasRepository;
        }

        public async Task IncluirCategoriaAsync(CategoriaInputModel categoria)
        {
            var novaCategoria = new CategoriaInputModel
            {
                Descricao = categoria.Descricao
            };

            await _categoriasRepository.IncluirCategoriaAsync(novaCategoria);
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategoriasAsync()
        {
            return await _categoriasRepository.ObterCategoriasAsync();
        }

        public Task<IEnumerable<CategoriaViewModel>> ObterCategoriasPorDescricaoAsync(string descricao)
        {
            throw new NotImplementedException();
        }

        public async Task ExcluirCategoriaAsync(Guid categoriaId)
        {
            await _categoriasRepository.ExcluirCategoriaAsync(categoriaId);
        }
    }
}
