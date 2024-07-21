using Financas.Pessoais.Domain.Models.InputModels;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Domain.FluntContratos
{
    public class DespesasUpdateModelContrato : Contract<Notification>
    {
        public DespesasUpdateModelContrato(DespesasUpdateModel despesasUpdateModel)
        {
            Requires()
            .IsGreaterThan(despesasUpdateModel.Valor, 0, "Valor", "O valor deve ser maior que zero")
            .IsNotNullOrEmpty(despesasUpdateModel.Descricao, "Descricao", "A descrição não pode ser vazia")
            .IsNotNullOrEmpty(despesasUpdateModel.Categoria, "Categoria", "A categoria não pode ser vazia")
            .IsTrue(despesasUpdateModel.DataVencimento > DateTime.MinValue, "DataVencimento", "Data de vencimento inválida")
            .IsLowerOrEqualsThan(despesasUpdateModel.Descricao, 100, "Descricao", "A descrição deve ter no máximo 100 caracteres");
        }
    }
}
