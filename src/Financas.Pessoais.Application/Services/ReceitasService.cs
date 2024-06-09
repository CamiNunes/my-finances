using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Enums;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;
using Financas.Pessoais.Infrastructure.Interfaces;
using Financas.Pessoais.Infrastructure.Repositories;

namespace Financas.Pessoais.Application.Services
{
    public class ReceitasService : IReceitasService
    {
        private readonly IReceitasRepository _receitasRepository;

        public ReceitasService(IReceitasRepository receitasRepository)
        {
            _receitasRepository = receitasRepository;
        }

        public async Task IncluirReceitaAsync(ReceitasInputModel receita)
        {
            //bool recebido = receita.Recebido=true;

            var novaReceita = new ReceitasInputModel
            {
                Descricao = receita.Descricao,
                TipoReceita = receita.TipoReceita == TipoReceitaEnum.Pessoal ? TipoReceitaEnum.Pessoal : TipoReceitaEnum.Casa,
                Valor = receita.Valor,
                Recebido = receita.Recebido, // Recebido agora é definido de acordo com o valor de "Sim" ou "Não"
                DataRecebimento = receita.DataRecebimento,
                Categoria = receita.Categoria
            };

            await _receitasRepository.IncluirReceitaAsync(novaReceita);
        }

        public async Task<IEnumerable<ReceitasViewModel>> ObterReceitasAsync()
        {
            return await _receitasRepository.ObterReceitasAsync();
        }

        public async Task<IEnumerable<ReceitasViewModel>> ObterReceitasPorDescricaoAsync(string descricao)
        {
            return await _receitasRepository.ObterReceitasPorDescricaoAsync(descricao);
        }

        public async Task ExcluirReceitaAsync(Guid receitaId)
        {
            await _receitasRepository.ExcluirReceitaAsync(receitaId);
        }
    }
}
