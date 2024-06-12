using Financas.Pessoais.Domain.Enums;

namespace Financas.Pessoais.Domain.Models.InputModels
{
    public class DespesasInputModel
    {
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public bool Pago { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public TipoDespesaEnum TipoDespesa { get; set; }
    }
}
