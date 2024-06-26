﻿using Dapper;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddUserAsync(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = "INSERT INTO TB_USUARIOS (Username, PasswordHash, Email) VALUES (@Username, @PasswordHash, @Email)";
                await connection.ExecuteAsync(query, user);
            }
        }

        public async Task<User> GetUserByUsernameAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = "SELECT * FROM TB_USUARIOS WHERE Email = @Email";
                return await connection.QuerySingleOrDefaultAsync<User>(query, new { Email = email });
            }
        }
    }
}
