using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Enums;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;
using Financas.Pessoais.Infrastructure.Interfaces;

namespace Financas.Pessoais.Application.Services
{
    public class CategoriasService : ICategoriasService
    {
        private readonly ICategoriasRepository _categoriasRepository;
        private readonly AuthService _authService;
        private readonly UserContext _userContext;

        public CategoriasService(ICategoriasRepository categoriasRepository, AuthService authService, UserContext userContext)
        {
            _categoriasRepository = categoriasRepository;
            _authService = authService;
            _userContext = userContext;
        }

        public async Task IncluirCategoriaAsync(Categoria categoria)
        {
            var usuario = await _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            var novaCategoria = new CategoriaInputModel
            {
                Descricao = categoria.Descricao
            };

            await _categoriasRepository.IncluirCategoriaAsync(novaCategoria, usuario.Email);
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategoriasAsync()
        {
            var usuario = await _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }
            return await _categoriasRepository.ObterCategoriasAsync(usuario.Email);
        }

        public Task<IEnumerable<CategoriaViewModel>> ObterCategoriasPorDescricaoAsync(string descricao)
        {
            throw new NotImplementedException();
        }

        public async Task ExcluirCategoriaAsync(Guid categoriaId)
        {
            var usuario = await _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }
            await _categoriasRepository.ExcluirCategoriaAsync(categoriaId, usuario.Email);
        }
    }
}
