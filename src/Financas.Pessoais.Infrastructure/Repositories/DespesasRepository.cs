using Dapper;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;
using Financas.Pessoais.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;

namespace Financas.Pessoais.Infrastructure.Repositories
{
    public class DespesasRepository : IDespesasRepository
    {
        private readonly string connectionString;

        public DespesasRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task IncluirDespesaAsync(DespesasInputModel despesa)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO TB_DESPESAS (Valor, Descricao, Pago, DataVencimento, DataPagamento, TipoDespesa, Categoria) VALUES (@Valor, @Descricao, @Pago, @DataVencimento, @DataPagamento, @TipoDespesa, @Categoria)";
                await connection.ExecuteAsync(sql, despesa);
            }
        }

        public async Task<IEnumerable<DespesasViewModel>> ObterDespesasAsync()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM TB_DESPESAS";
                return await connection.QueryAsync<DespesasViewModel>(sql);
            }
        }

        public async Task<IEnumerable<DespesasViewModel>> ObterDespesasPorDescricaoAsync(string descricao)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                // Corrigindo a consulta SQL para usar o parâmetro e a cláusula LIKE corretamente
                var sql = "SELECT * FROM TB_DESPESAS WHERE Descricao LIKE '%' + @Descricao + '%'";
                return await connection.QueryAsync<DespesasViewModel>(sql, new { Descricao = descricao });
            }
        }
    }
}
