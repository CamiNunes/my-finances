﻿using Financas.Pessoais.Application.Services;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models;
using Financas.Pessoais.Infrastructure.Interfaces;
using Financas.Pessoais.Infrastructure.Seguranca;
using Microsoft.AspNetCore.Mvc;

namespace Financas.Pessoais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService authService, IUserRepository userRepository, ILogger<AuthController> logger)
        {
            _authService = authService;
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                _logger.LogInformation("Tentativa de login para o email: {Email}", loginModel.Email);

                var (token, userName) = await _authService.AuthenticateAsync(loginModel.Email, loginModel.Password);
                if (token == null || userName == null)
                {
                    _logger.LogWarning("Login falhou para o email: {Email}", loginModel.Email);
                    return Unauthorized();
                }

                _logger.LogInformation("Login bem-sucedido para o email: {Email}", loginModel.Email);
                return Ok(new { Token = token, UserName = userName });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar fazer login para o email: {Email}", loginModel.Email);
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            try
            {
                _logger.LogInformation("Registrando novo usuário: {Username}", registerModel.Username);

                bool emailExists = await _authService.VerificarSeEmailExisteAsync(registerModel.Email);
                if (emailExists)
                {
                    _logger.LogWarning("Tentativa de registrar novo usuário com email existente: {Email}", registerModel.Email);
                    return Conflict("Este email já está cadastrado.");
                }

                string passwordHash = PasswordHasher.HashPassword(registerModel.Password);
                var user = new User
                {
                    Username = registerModel.Username,
                    PasswordHash = passwordHash,
                    Email = registerModel.Email
                };

                await _userRepository.AddUserAsync(user);

                _logger.LogInformation("Novo usuário registrado: {Username}", registerModel.Username);
                return Ok("Usuário cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar registrar novo usuário: {Username}", registerModel.Username);
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
