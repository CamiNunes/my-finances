using Dapper;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;
using Financas.Pessoais.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;

namespace Financas.Pessoais.Infrastructure.Repositories
{
    public class ReceitasRepository : IReceitasRepository
    {
        private readonly string connectionString;

        public ReceitasRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task IncluirReceitaAsync(ReceitasInputModel receitas)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO TB_RECEITAS (Valor, Descricao, Recebido, DataRecebimento, TipoReceita, Categoria) VALUES (@Valor, @Descricao, @Recebido, @DataRecebimento, @TipoReceita, @Categoria)";
                await connection.ExecuteAsync(sql, receitas);
            }
        }

        public async Task<IEnumerable<ReceitasViewModel>> ObterReceitasAsync()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM TB_RECEITAS";
                return await connection.QueryAsync<ReceitasViewModel>(sql);
            }
        }

        public async Task<IEnumerable<ReceitasViewModel>> ObterReceitasPorDescricaoAsync(string descricao)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                // Corrigindo a consulta SQL para usar o parâmetro e a cláusula LIKE corretamente
                var sql = "SELECT * FROM TB_RECEITAS WHERE Descricao LIKE '%' + @Descricao + '%'";
                return await connection.QueryAsync<ReceitasViewModel>(sql, new { Descricao = descricao });
            }
        }
    }
}
