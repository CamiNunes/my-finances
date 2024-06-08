namespace Financas.Pessoais.Domain.Entidades
{
    public class Categoria
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public Categoria() => Id = Guid.NewGuid();
    }
}
