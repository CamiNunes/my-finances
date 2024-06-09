using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models.InputModels;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Pessoais.Domain.FluntContracts
{
    public class CategoriaInputModelContrato : Contract<Notification>
    {
        public CategoriaInputModelContrato(CategoriaInputModel categoria)
        {
            Requires()
                .IsNotNullOrEmpty(categoria.Descricao, "Descricao", "A descrição não pode ser vazia")
                .IsGreaterThan(categoria.Descricao, 3, "Descricao", "A descrição deve ter mais de 2 caracteres")
                .IsLowerOrEqualsThan(categoria.Descricao, 50, "Descricao", "A descrição deve ter no máximo 100 caracteres");
        }
    }
}
