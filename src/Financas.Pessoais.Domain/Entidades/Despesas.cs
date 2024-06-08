using Financas.Pessoais.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Domain.Entidades
{
    public class Despesas : Lancamento
    {
        public bool Pago { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public TipoDespesaEnum Tipo { get; set; }
    }
}
