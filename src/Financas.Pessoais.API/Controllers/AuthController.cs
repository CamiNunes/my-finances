using Financas.Pessoais.Application.Services;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models;
using Financas.Pessoais.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Financas.Pessoais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IUserRepository _userRepository;

        public AuthController(AuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var (token, userName) = await _authService.AuthenticateAsync(loginModel.Email, loginModel.Password);
            if (token == null || userName == null)
            {
                return Unauthorized();
            }
            return Ok(new { Token = token, UserName = userName });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            // Simplified for example purposes, you'd hash the password in a real application
            var user = new User
            {
                Username = registerModel.Username,
                PasswordHash = registerModel.Password,
                Email = registerModel.Email
            };

            await _userRepository.AddUserAsync(user);
            return Ok();
        }
    }
}
