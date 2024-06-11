using Financas.Pessoais.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Domain.Entidades
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public TipoUsuario TipoUsuario { get; set; }

        // Propriedade calculada para determinar se o usuário é administrador
        public bool IsAdmin => TipoUsuario == TipoUsuario.Administrador;

        // Construtor padrão
        public User() { }

        // Construtor que inicializa a instância com parâmetros
        public User(string username, string passwordHash, string email, TipoUsuario tipoUsuario)
        {
            Id = Guid.NewGuid();
            Username = username;
            PasswordHash = passwordHash;
            Email = email;
            TipoUsuario = tipoUsuario;
        }

        // Método para inicializar ou atualizar o usuário
        public void InitializeUser(string username, string passwordHash, string email, TipoUsuario tipoUsuario)
        {
            Username = username;
            PasswordHash = passwordHash;
            Email = email;
            TipoUsuario = tipoUsuario;
        }
    }
}
