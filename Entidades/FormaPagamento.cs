using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Domain.Entidades
{
    public class FormaPagamento
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public FormaPagamento() => Id = Guid.NewGuid();
    }
}
