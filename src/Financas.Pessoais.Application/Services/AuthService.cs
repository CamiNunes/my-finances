using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models;
using Financas.Pessoais.Infrastructure.Interfaces;
using Financas.Pessoais.Infrastructure.Seguranca;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _jwtKey = configuration["JwtSettings:SecretKey"];
            _jwtIssuer = configuration["JwtSettings:Issuer"];
            _jwtAudience = configuration["JwtSettings:Audience"];
            _httpContextAccessor = httpContextAccessor;

            if (string.IsNullOrEmpty(_jwtKey))
            {
                throw new ArgumentNullException(nameof(_jwtKey), "JWT key is not configured.");
            }
        }

        public Task<UsuarioViewModel> ObterUsuarioAutenticadoAsync()
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                var email = user.FindFirst(ClaimTypes.Email)?.Value ?? user.FindFirst("email")?.Value;
                var isAdmin = user.IsInRole("Administrador") || user.FindFirst(ClaimTypes.Role)?.Value == "Administrador";

                return Task.FromResult(new UsuarioViewModel
                {
                    Email = email,
                    IsAdmin = isAdmin
                });
            }

            return Task.FromResult<UsuarioViewModel>(null);
        }

        public async Task<bool> VerificarSeEmailExisteAsync(string email)
        {
            var existingUser = await _userRepository.ObterUsuarioPorEmailAsync(email);
            return existingUser != null;
        }

        public async Task<(string token, string userName)> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.ObterUsuarioPorEmailAsync(username);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return (null, null);
            }

            var token = GenerateToken(user);

            return (token, user.Username);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            string hashedPassword = PasswordHasher.HashPassword(password);
            return hashedPassword == storedHash; // Simplified for example purposes
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtKey);
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("email", user.Email)
        };

            if (user.IsAdmin)
            {
                claims.Add(new Claim("role", "Administrador"));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _jwtIssuer,
                Audience = _jwtAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
