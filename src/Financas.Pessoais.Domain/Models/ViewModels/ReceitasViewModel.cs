using Financas.Pessoais.Domain.Enums;

namespace Financas.Pessoais.Domain.Models.ViewModels
{
    public class ReceitasViewModel
    {
        public Guid Id { get; set; }
        public bool Recebido { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime? DataRecebimento { get; set; }
        public TipoReceitaEnum TipoReceita { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string StatusReceita { get; set; } = string.Empty;
    }
}
