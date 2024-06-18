using AutoMapper;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;

namespace Financas.Pessoais.Domain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoriaInputModel, Categoria>();

            CreateMap<DespesasInputModel, Despesas>()
                .ForMember(dest => dest.DataPagamento, opt => opt.MapFrom(src => src.DataPagamento));

            CreateMap<ReceitasInputModel, Receitas>();

            CreateMap<DespesasViewModel, Despesas>();
        }
    }
}
    