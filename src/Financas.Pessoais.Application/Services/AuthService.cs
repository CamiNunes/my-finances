using Financas.Pessoais.Infrastructure.Interfaces;
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

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _jwtKey = configuration["JwtSettings:SecretKey"];
            _jwtIssuer = configuration["JwtSettings:Issuer"];
            _jwtAudience = configuration["JwtSettings:Audience"];

            if (string.IsNullOrEmpty(_jwtKey))
            {
                throw new ArgumentNullException(nameof(_jwtKey), "JWT key is not configured.");
            }
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

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, user.Username)
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _jwtIssuer,
                Audience = _jwtAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return (tokenString, user.Username);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            // Implement your password verification logic here
            return password == storedHash; // Simplified for example purposes
        }
    }
}
