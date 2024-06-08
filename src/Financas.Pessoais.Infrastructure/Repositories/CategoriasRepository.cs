using Dapper;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Infrastructure.Repositories
{
    public class CategoriasRepository : ICategoriasRepository
    {
        private readonly string connectionString;

        public CategoriasRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task IncluirCategoriaAsync(Categoria categoria)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO TB_CATEGORIAS (Descricao, DataCriacao) VALUES (@Descricao, @DataCriacao)";
                await connection.ExecuteAsync(sql, categoria);
            }
        }

        public async Task<IEnumerable<Categoria>> ObterCategoriasAsync()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM TB_CATEGORIAS ORDER BY DESCRICAO";
                return await connection.QueryAsync<Categoria>(sql);
            }
        }

        public Task<IEnumerable<Categoria>> ObterCategoriasPorDescricaoAsync(string descricao)
        {
            throw new NotImplementedException();
        }

        public async Task ExcluirCategoriaAsync(Guid categoriaId)
        {
            const string query = "DELETE FROM TB_CATEGORIAS WHERE Id = @CategoriaId";

            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var parameters = new { CategoriaId = categoriaId };
                await dbConnection.ExecuteAsync(query, parameters);
            }
        }
    }
}
