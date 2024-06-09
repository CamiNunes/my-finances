using Financas.Pessoais.Domain.Enums;

namespace Financas.Pessoais.Domain.Models.InputModels
{
    public  class ReceitasInputModel
    {
        public bool Recebido { get; set; }
        public DateTime? DataRecebimento { get; set; }
        public TipoReceitaEnum TipoReceita { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
    }
}
