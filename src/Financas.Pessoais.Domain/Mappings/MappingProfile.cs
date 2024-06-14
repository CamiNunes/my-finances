using AutoMapper;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Financas.Pessoais.Domain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoriaInputModel, Categoria>();
            CreateMap<DespesasInputModel, Despesas>();
            CreateMap<ReceitasInputModel, Receitas>();
        }
    }
}
