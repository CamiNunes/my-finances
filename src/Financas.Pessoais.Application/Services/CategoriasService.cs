using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Enums;
using Financas.Pessoais.Infrastructure.Interfaces;
using Financas.Pessoais.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Application.Services
{
    public class CategoriasService : ICategoriasService
    {
        private readonly ICategoriasRepository _categoriasRepository;

        public CategoriasService(ICategoriasRepository categoriasRepository)
        {
            _categoriasRepository = categoriasRepository;
        }

        public async Task IncluirCategoriaAsync(Categoria categoria)
        {
            var novaCategoria = new Categoria
            {
                Descricao = categoria.Descricao,
                DataCriacao = categoria.DataCriacao,
            };

            await _categoriasRepository.IncluirCategoriaAsync(novaCategoria);
        }

        public async Task<IEnumerable<Categoria>> ObterCategoriasAsync()
        {
            return await _categoriasRepository.ObterCategoriasAsync();
        }

        public Task<IEnumerable<Categoria>> ObterCategoriasPorDescricaoAsync(string descricao)
        {
            throw new NotImplementedException();
        }

        public async Task ExcluirCategoriaAsync(Guid categoriaId)
        {
            await _categoriasRepository.ExcluirCategoriaAsync(categoriaId);
        }
    }
}
