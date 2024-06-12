using Financas.Pessoais.Domain.Models.InputModels;
using Flunt.Notifications;
using Flunt.Validations;

namespace Financas.Pessoais.Domain.FluntContratos
{
    public class DespesasInputModelContrato : Contract<Notification>
    {
        public DespesasInputModelContrato(DespesasInputModel despesasInputModel)
        {
            Requires()
            .IsGreaterThan(despesasInputModel.Valor, 0, "Valor", "O valor deve ser maior que zero")
            .IsNotNullOrEmpty(despesasInputModel.Descricao, "Descricao", "A descrição não pode ser vazia")
            .IsNotNullOrEmpty(despesasInputModel.Categoria, "Categoria", "A categoria não pode ser vazia")
            .IsTrue(despesasInputModel.DataVencimento > DateTime.MinValue, "DataVencimento", "Data de vencimento inválida")
            .IsLowerOrEqualsThan(despesasInputModel.Descricao, 100, "Descricao", "A descrição deve ter no máximo 100 caracteres");

            if (despesasInputModel.Pago)
            {
                Requires()
                    .IsTrue(despesasInputModel.DataPagamento != null && despesasInputModel.DataPagamento.Value > DateTime.MinValue, "DataPagamento", "Data de pagamento deve ser fornecida quando a despesa está paga");
            }
        }
    }
}
