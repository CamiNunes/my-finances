using Financas.Pessoais.Domain.Enums;

namespace Financas.Pessoais.Domain.Models.ViewModels
{
    public class DespesasViewModel
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public bool Pago { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public TipoDespesaEnum Tipo { get; set; }
    }
}
