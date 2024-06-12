using Financas.Pessoais.Application.Services;
using Financas.Pessoais.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Application
{
    public class UserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthService _authService;
        private UsuarioViewModel _cachedUser;

        public UserContext(IHttpContextAccessor httpContextAccessor, AuthService authService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
        }

        public async Task<UsuarioViewModel> GetAuthenticatedUserAsync()
        {
            if (_cachedUser == null)
            {
                _cachedUser = await _authService.ObterUsuarioAutenticadoAsync();
            }
            return _cachedUser;
        }

        public async Task<UsuarioViewModel> GetAuthenticatedAdminUserAsync()
        {
            var user = await GetAuthenticatedUserAsync();
            if (user == null || !user.IsAdmin)
            {
                throw new UnauthorizedAccessException("Usuário não tem permissão para realizar esta ação.");
            }
            return user;
        }
    }
}
