using Financas.Pessoais.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Application.Interfaces
{
    public interface IDespesasService
    {
        Task IncluirDespesaAsync(Despesas receita);
    }
}
