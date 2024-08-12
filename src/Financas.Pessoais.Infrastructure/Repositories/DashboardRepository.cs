using Dapper;
using Financas.Pessoais.Domain.DTOs;
using Financas.Pessoais.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly string _connectionString;

        public DashboardRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public decimal ObterSomaDasDespesasDoMes(int mes, string emailUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Consulta SQL para obter a soma das despesas do mês
                string query = @"
                                SELECT ISNULL(SUM(VALOR), 0) AS VALOR_DESPESA
                                FROM TB_DESPESAS
                                WHERE MONTH(DATAVENCIMENTO) = @Mes AND 
                                      PAGO = 1 AND 
                                      CriadoPor = @EmailUsuario";

                decimal sum = connection.ExecuteScalar<decimal>(query, new { Mes = mes, EmailUsuario = emailUsuario });

                return sum;
            }
        }

        public decimal ObterSomaDasDespesasEmAbertoDoMes(int mes, string emailUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Consulta SQL para obter a soma das despesas do mês
                string query = @"
                                SELECT ISNULL(SUM(VALOR), 0) AS VALOR_DESPESA
                                FROM TB_DESPESAS
                                WHERE MONTH(DATAVENCIMENTO) = @Mes AND 
                                      PAGO = 0 AND 
                                      CriadoPor = @EmailUsuario";

                decimal sum = connection.ExecuteScalar<decimal>(query, new { Mes = mes, EmailUsuario = emailUsuario });

                return sum;
            }
        }

        public int ObterQuantidadeDespesasProximasVencimento(int mes, string emailUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Consulta SQL para obter a quantidade de despesas próximas do vencimento
                string query = @"
                                SELECT COUNT(*) AS QTD_DESPESAS_PROXIMAS
                                FROM TB_DESPESAS
                                WHERE DATEDIFF(day, GETDATE(), DATAVENCIMENTO) <= @Mes
                                  AND DATEDIFF(day, GETDATE(), DATAVENCIMENTO) >= 0
                                  AND CriadoPor = @EmailUsuario";

                // Executando a consulta usando Dapper
                int count = connection.ExecuteScalar<int>(query, new { Mes = mes, EmailUsuario = emailUsuario });

                return count;
            }
        }

        public decimal ObterSomaDasReceitasDoMes(int mes, string emailUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Consulta SQL para obter a soma das despesas do mês
                string query = @"
                                SELECT ISNULL(SUM(VALOR), 0) AS VALOR_RECEITA
                                FROM TB_RECEITAS
                                WHERE MONTH(DATARECEBIMENTO) = @Mes AND 
                                      RECEBIDO = 1 AND 
                                      CriadoPor = @EmailUsuario";

                decimal sum = connection.ExecuteScalar<decimal>(query, new { Mes = mes, EmailUsuario = emailUsuario });

                return sum;
            }
        }

        public decimal ObterDiferencaReceitasDespesas(int mes, string emailUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Consulta SQL para obter a diferença entre receitas e despesas pagas no mês específico
                string query = @"
                                SELECT 
                                    (ISNULL((SELECT SUM(VALOR) 
                                             FROM TB_RECEITAS 
                                             WHERE CriadoPor = @EmailUsuario AND RECEBIDO = 1 AND MONTH(DATARECEBIMENTO) = @Mes), 0) 
                                    - ISNULL((SELECT SUM(VALOR) 
                                              FROM TB_DESPESAS 
                                              WHERE CriadoPor = @EmailUsuario AND PAGO = 1 AND MONTH(DATAVENCIMENTO) = @Mes), 0)) AS DIFERENCA";

                // Executando a consulta usando Dapper
                decimal diferenca = connection.ExecuteScalar<decimal>(query, new { EmailUsuario = emailUsuario, Mes = mes });

                return diferenca;
            }
        }

        public List<DespesasDashboardDTO> ListarContasEmAberto(int mes, string emailUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
                                  SELECT 
	                                DESCRICAO, VALOR, DATAVENCIMENTO 
                                  FROM 
	                                TB_DESPESAS 
                                  WHERE 
	                                MONTH(DATAVENCIMENTO) = @Mes AND 
                                    CriadoPor = @EmailUsuario AND
	                                Pago = 0";

                var despesas = connection.Query<DespesasDashboardDTO>(query, new { Mes = mes, EmailUsuario = emailUsuario }).ToList();

                return despesas;
            }
        }
    }
}
