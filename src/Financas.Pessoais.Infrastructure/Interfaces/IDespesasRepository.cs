using Financas.Pessoais.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Infrastructure.Interfaces
{
    public interface IDespesasRepository
    {
        Task IncluirDespesaAsync(Despesas receita);
    }
}
