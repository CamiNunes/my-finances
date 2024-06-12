using Financas.Pessoais.Domain.Models.InputModels;
using Flunt.Notifications;
using Flunt.Validations;

namespace Financas.Pessoais.Domain.FluntContratos
{
    public class ReceitasInputModelContrato : Contract<Notification>
    {
        public ReceitasInputModelContrato(ReceitasInputModel receitasInputModel)
        {
            Requires()
                .IsGreaterThan(receitasInputModel.Valor, 0, "Valor", "O valor deve ser maior que zero")
                .IsNotNullOrEmpty(receitasInputModel.Descricao, "Descricao", "A descrição não pode ser vazia")
                .IsNotNullOrEmpty(receitasInputModel.Categoria, "Categoria", "A categoria não pode ser vazia")
                .IsLowerOrEqualsThan(receitasInputModel.Descricao, 100, "Descricao", "A descrição deve ter no máximo 100 caracteres");

            if (receitasInputModel.Recebido)
            {
                Requires()
                    .IsTrue(receitasInputModel.DataRecebimento != null && receitasInputModel.DataRecebimento.Value > DateTime.MinValue, "DataRecebimento", "Data de recebimento deve ser fornecida quando a despesa está recebida.");
            }
        }
    }
}
