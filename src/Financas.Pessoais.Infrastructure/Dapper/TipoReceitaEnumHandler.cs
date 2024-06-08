using Dapper;
using Financas.Pessoais.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Infrastructure.Dapper
{
    public class TipoReceitaEnumHandler : SqlMapper.TypeHandler<TipoReceitaEnum>
    {
        public override void SetValue(IDbDataParameter parameter, TipoReceitaEnum value)
        {
            parameter.Value = (int)value;
        }

        public override TipoReceitaEnum Parse(object value)
        {
            return (TipoReceitaEnum)Enum.ToObject(typeof(TipoReceitaEnum), value);
        }
    }
}
