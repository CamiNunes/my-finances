using Dapper;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;
using Financas.Pessoais.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Financas.Pessoais.Infrastructure.Repositories
{
    public class ReceitasRepository : IReceitasRepository
    {
        private readonly string connectionString;

        public ReceitasRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task IncluirReceitaAsync(ReceitasInputModel receita, string emailUsuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO TB_RECEITAS (Valor, Descricao, Recebido, DataRecebimento, TipoReceita, Categoria, CriadoPor) VALUES (@Valor, @Descricao, @Recebido, @DataRecebimento, @TipoReceita, @Categoria, @CriadoPor)";
                var parameters = new
                {
                    receita.Valor,
                    receita.Descricao,
                    receita.Recebido,
                    DataRecebimento = receita.DataRecebimento ?? (object)DBNull.Value,
                    receita.TipoReceita,
                    receita.Categoria,
                    CriadoPor = emailUsuario
                };
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<IEnumerable<ReceitasViewModel>> ObterReceitasAsync(string emailUsuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM TB_RECEITAS WHERE CriadoPor = @CriadoPor";
                return await connection.QueryAsync<ReceitasViewModel>(sql, new { CriadoPor = emailUsuario });
            }
        }

        public async Task<IEnumerable<ReceitasViewModel>> ObterReceitasPorDescricaoAsync(string descricao, string emailUsuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                // Corrigindo a consulta SQL para usar o parâmetro e a cláusula LIKE corretamente
                var sql = "SELECT * FROM TB_RECEITAS WHERE CriadoPor = @CriadoPor and Descricao Descricao LIKE '%' + @Descricao + '%'";
                return await connection.QueryAsync<ReceitasViewModel>(sql, new { Descricao = descricao, CriadoPor = emailUsuario });
            }
        }

        public async Task ExcluirReceitaAsync(Guid receitaId, string emailUsuario)
        {
            const string query = "DELETE FROM TB_RECEITAS WHERE Id = @ReceitaId AND CriadoPor = @CriadoPor";

            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var parameters = new { ReceitaId = receitaId, CriadoPor = emailUsuario };
                await dbConnection.ExecuteAsync(query, parameters);
            }
        }
    }
}
