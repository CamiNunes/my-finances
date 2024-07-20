using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Domain.DTOs
{
    public class DespesasDashboardDTO
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor {  get; set; } = decimal.Zero;
        public DateTime? DataVencimento { get; set; }
    }
}
