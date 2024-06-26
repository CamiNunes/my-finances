﻿using Dapper;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;
using Financas.Pessoais.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace Financas.Pessoais.Infrastructure.Repositories
{
    public class DespesasRepository : IDespesasRepository
    {
        private readonly string connectionString;

        public DespesasRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task IncluirDespesaAsync(DespesasInputModel despesa, string emailUsuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO TB_DESPESAS (Valor, Descricao, Pago, DataVencimento, DataPagamento, TipoDespesa, Categoria, CriadoPor) VALUES (@Valor, @Descricao, @Pago, @DataVencimento, @DataPagamento, @TipoDespesa, @Categoria, @CriadoPor)";
                var parameters = new { 
                            Valor = despesa.Valor,
                            Descricao = despesa.Descricao,
                            Pago = despesa.Pago,
                            DataVencimento = despesa.DataVencimento,
                            DataPagamento = despesa.DataPagamento.HasValue && despesa.DataPagamento.Value >= (DateTime)SqlDateTime.MinValue ? despesa.DataPagamento.Value : (object)DBNull.Value,
                            TipoDespesa = despesa.TipoDespesa,
                            Categoria = despesa.Categoria,
                            CriadoPor = emailUsuario };
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<IEnumerable<Despesas>> ObterDespesasAsync(string emailUsuario, int? mes = null, string status = null, string descricao = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM TB_DESPESAS WHERE CriadoPor = @CriadoPor";
                var parameters = new DynamicParameters();
                parameters.Add("CriadoPor", emailUsuario);

                if (mes.HasValue)
                {
                    sql += " AND MONTH(DataVencimento) = @Mes";
                    parameters.Add("Mes", mes.Value);
                }

                if (!string.IsNullOrEmpty(status))
                {
                    if (status.Equals("PAGO", StringComparison.OrdinalIgnoreCase))
                    {
                        sql += " AND Pago = 1";
                    }
                    else if (status.Equals("VENCIDO", StringComparison.OrdinalIgnoreCase))
                    {
                        sql += " AND Pago = 0 AND DataVencimento <= @DataAtual";
                        parameters.Add("DataAtual", DateTime.UtcNow);
                    }
                    else if (status.Equals("ABERTO", StringComparison.OrdinalIgnoreCase))
                    {
                        sql += " AND Pago = 0 AND DataVencimento > @DataAtual";
                        parameters.Add("DataAtual", DateTime.UtcNow);
                    }
                }

                if (!string.IsNullOrEmpty(descricao))
                {
                    sql += " AND Descricao LIKE @Descricao";
                    parameters.Add("Descricao", "%" + descricao + "%");
                }

                return await connection.QueryAsync<Despesas>(sql, parameters);
            }
        }

        public async Task<IEnumerable<Despesas>> ObterDespesasAsync(string emailUsuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM TB_DESPESAS WHERE CriadoPor = @CriadoPor";
                return await connection.QueryAsync<Despesas>(sql, new {  CriadoPor = emailUsuario });
            }
        }

        public async Task<IEnumerable<DespesasViewModel>> ObterDespesasPorDescricaoAsync(string descricao, string emailUsuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                // Corrigindo a consulta SQL para usar o parâmetro e a cláusula LIKE corretamente
                var sql = "SELECT * FROM TB_DESPESAS WHERE CriadoPor = @CriadoPor and Descricao LIKE '%' + @Descricao + '%'";
                return await connection.QueryAsync<DespesasViewModel>(sql, new { Descricao = descricao, CriadoPor = emailUsuario });
            }
        }

        public async Task ExcluirDespesaAsync(Guid despesaId, string emailUsuario)
        {
            const string query = "DELETE FROM TB_DESPESAS WHERE Id = @DespesaId AND CriadoPor = @CriadoPor";

            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var parameters = new { DespesaId = despesaId, CriadoPor = emailUsuario };
                await dbConnection.ExecuteAsync(query, parameters);
            }
        }
    }
}
