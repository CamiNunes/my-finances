using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Enums;
using Financas.Pessoais.Infrastructure.Interfaces;
using System.Drawing;

namespace Financas.Pessoais.Application.Services
{
    public class ReceitasService : IReceitasService
    {
        private readonly IReceitasRepository _receitasRepository;

        public ReceitasService(IReceitasRepository receitasRepository)
        {
            _receitasRepository = receitasRepository;
        }

        public async Task IncluirReceitaAsync(Receitas receita)
        {
            bool recebido = receita.Recebido=true;

            var novaReceita = new Receitas
            {
                Descricao = receita.Descricao,
                TipoReceita = receita.TipoReceita == TipoReceitaEnum.Pessoal ? TipoReceitaEnum.Pessoal : TipoReceitaEnum.Casa,
                Valor = receita.Valor,
                DataLancamento = receita.DataLancamento,
                Recebido = recebido, // Recebido agora é definido de acordo com o valor de "Sim" ou "Não"
                DataRecebimento = receita.DataRecebimento,
                Categoria = receita.Categoria
            };

            await _receitasRepository.IncluirReceitaAsync(novaReceita);
        }

        public async Task<IEnumerable<Receitas>> ObterReceitasAsync()
        {
            return await _receitasRepository.ObterReceitasAsync();
        }

        public async Task<IEnumerable<Receitas>> ObterReceitasPorDescricaoAsync(string descricao)
        {
            return await _receitasRepository.ObterReceitasPorDescricaoAsync(descricao);
        }
    }
}
