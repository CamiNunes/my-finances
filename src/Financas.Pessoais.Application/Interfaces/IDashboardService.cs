using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Application.Interfaces
{
    public interface IDashboardService
    {
        decimal ObterSomaDasDespesasDoMes(int mes);
        int ObterQuantidadeDespesasProximasVencimento(int mes);
        decimal ObterSomaDasReceitasDoMes(int mes);
        decimal ObterDiferencaReceitasDespesas(int mes);
    }
}
