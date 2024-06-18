using Financas.Pessoais.Domain.Enums;

namespace Financas.Pessoais.Domain.Entidades
{
    public class Despesas : Lancamento
    {
        public bool Pago { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public TipoDespesaEnum TipoDespesa { get; set; }
    }
}
