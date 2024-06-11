using Dapper;
using Financas.Pessoais.Domain.Entidades;
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

        public async Task IncluirCategoriaAsync(CategoriaInputModel categoria, string emailUsuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO TB_CATEGORIAS (Descricao, CriadoPor) VALUES (@Descricao, @CriadoPor)";
                var parameters = new { Descricao = categoria.Descricao, CriadoPor = emailUsuario };
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategoriasAsync(string emailUsuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM TB_CATEGORIAS WHERE CriadoPor = @CriadoPor ORDER BY DESCRICAO ";
                var parameters = new { CriadoPor = emailUsuario };
                return await connection.QueryAsync<CategoriaViewModel>(sql, parameters);
            }
        }

        public Task<IEnumerable<CategoriaViewModel>> ObterCategoriasPorDescricaoAsync(string descricao)
        {
            throw new NotImplementedException();
        }

        public async Task ExcluirCategoriaAsync(Guid categoriaId, string emailUsuario)
        {
            const string query = "DELETE FROM TB_CATEGORIAS WHERE Id = @CategoriaId AND CriadoPor = @CriadoPor";

            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var parameters = new { CategoriaId = categoriaId, CriadoPor = emailUsuario };
                await dbConnection.ExecuteAsync(query, parameters);
            }
        }
    }
}
