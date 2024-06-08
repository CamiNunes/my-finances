using Financas.Pessoais.Domain.Models.InputModels;

namespace Financas.Pessoais.Application.Interfaces
{
    public interface IDespesasService
    {
        Task IncluirDespesaAsync(DespesasInputModel receita);
    }
}
