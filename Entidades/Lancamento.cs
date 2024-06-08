using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Domain.Entidades
{
    public abstract class Lancamento
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataVenecimento { get; set; }
        public DateTime DataPagamento { get; set; }

        public Lancamento() => Id = Guid.NewGuid();
    }
}
