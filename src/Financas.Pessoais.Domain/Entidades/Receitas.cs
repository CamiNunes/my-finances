using Financas.Pessoais.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Domain.Entidades
{
    public class Receitas : Lancamento
    {
        public bool Recebido { get; set; }
        public DateTime DataRecebimento { get; set; }
        public TipoReceitaEnum TipoReceita { get; set; }
    }
}
