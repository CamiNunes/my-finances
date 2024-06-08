using Dapper;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;
using Financas.Pessoais.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Financas.Pessoais.Infrastructure.Repositories
{
    public class CategoriasRepository : ICategoriasRepository
    {
        private readonly string connectionString;

        public CategoriasRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task IncluirCategoriaAsync(CategoriaInputModel categoria)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO TB_CATEGORIAS (Descricao) VALUES (@Descricao)";
                await connection.ExecuteAsync(sql, categoria);
            }
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategoriasAsync()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM TB_CATEGORIAS ORDER BY DESCRICAO";
                return await connection.QueryAsync<CategoriaViewModel>(sql);
            }
        }

        public Task<IEnumerable<CategoriaViewModel>> ObterCategoriasPorDescricaoAsync(string descricao)
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
