namespace Financas.Pessoais.Domain.Entidades
{
    public abstract class Lancamento
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataLancamento { get; set; } = DateTime.Now;
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;

        public Lancamento() => Id = Guid.NewGuid();
    }
}
