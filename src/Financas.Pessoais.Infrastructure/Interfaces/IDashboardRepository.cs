using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Infrastructure.Interfaces
{
    public interface IDashboardRepository
    {
        decimal ObterSomaDasDespesasDoMes(int mes, string emailUsuario);
        decimal ObterSomaDasDespesasEmAbertoDoMes(int mes, string emailUsuario);
        int ObterQuantidadeDespesasProximasVencimento(int mes, string emailUsuario);
        decimal ObterSomaDasReceitasDoMes(int mes, string emailUsuario);
        decimal ObterDiferencaReceitasDespesas(int mes, string emailUsuario);
    }
}
