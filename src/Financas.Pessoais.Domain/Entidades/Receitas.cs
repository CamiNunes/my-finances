using Financas.Pessoais.Domain.Enums;

namespace Financas.Pessoais.Domain.Entidades
{
    public class Receitas : Lancamento
    {
        public bool Recebido { get; set; }
        public DateTime? DataRecebimento { get; set; }
        public TipoReceitaEnum TipoReceita { get; set; }
    }
}
