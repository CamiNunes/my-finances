using Financas.Pessoais.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Domain.Models.InputModels
{
    public class DespesasUpdateModel
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public bool Pago { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public TipoDespesaEnum TipoDespesa { get; set; }
        //public string ModificadoPor { get; set; } = string.Empty;
        //public DateTime? DataModificacao { get; set;} = DateTime.Now;
    }
}
