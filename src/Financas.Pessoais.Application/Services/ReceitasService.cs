using Financas.Pessoais.Application.Interfaces;
using Financas.Pessoais.Domain.Entidades;
using Financas.Pessoais.Domain.Enums;
using Financas.Pessoais.Domain.Models.InputModels;
using Financas.Pessoais.Domain.Models.ViewModels;
using Financas.Pessoais.Infrastructure.Interfaces;

namespace Financas.Pessoais.Application.Services
{
    public class ReceitasService : IReceitasService
    {
        private readonly IReceitasRepository _receitasRepository;
        private readonly UserContext _userContext;

        public ReceitasService(IReceitasRepository receitasRepository, UserContext userContext)
        {
            _receitasRepository = receitasRepository;
            _userContext = userContext;
        }

        public async Task IncluirReceitaAsync(Receitas receita)
        {
            var usuario = await _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            var novaReceita = new ReceitasInputModel
            {
                Descricao = receita.Descricao,
                TipoReceita = receita.TipoReceita == TipoReceitaEnum.Pessoal ? TipoReceitaEnum.Pessoal : TipoReceitaEnum.Casa,
                Valor = receita.Valor,
                Recebido = receita.Recebido, // Recebido agora é definido de acordo com o valor de "Sim" ou "Não"
                DataRecebimento = receita.DataRecebimento,
                Categoria = receita.Categoria
            };

            await _receitasRepository.IncluirReceitaAsync(novaReceita, usuario.Email);
        }

        public async Task<IEnumerable<ReceitasViewModel>> ObterReceitasAsync()
        {
            var usuario = await _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            var receitas = await _receitasRepository.ObterReceitasAsync(usuario.Email);

            var receitasViewModel = receitas.Select(receita => new ReceitasViewModel
            {
                Id = receita.Id,
                Descricao = receita.Descricao,
                TipoReceita = receita.TipoReceita == TipoReceitaEnum.Pessoal ? TipoReceitaEnum.Pessoal : TipoReceitaEnum.Casa,
                Valor = receita.Valor,
                Recebido = receita.Recebido, // Recebido agora é definido de acordo com o valor de "Sim" ou "Não"
                DataRecebimento = receita.DataRecebimento,
                Categoria = receita.Categoria,
                StatusReceita = receita.Recebido ? "RECEBIDO"
                                : receita.DataRecebimento <= DateTime.UtcNow ? "VENCIDO"
                                : "ABERTO"
            }).ToList();

            return receitasViewModel;
        }

        //public async Task<IEnumerable<ReceitasViewModel>> ObterReceitasAsync()
        //{
        //    var usuario = await _userContext.GetAuthenticatedUserAsync();
        //    if (usuario == null)
        //    {
        //        throw new UnauthorizedAccessException("Usuário não autenticado.");
        //    }

        //    return await _receitasRepository.ObterReceitasAsync(usuario.Email);
        //}

        public async Task<IEnumerable<ReceitasViewModel>> ObterReceitasPorDescricaoAsync(string descricao)
        {
            var usuario = await _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            return await _receitasRepository.ObterReceitasPorDescricaoAsync(descricao, usuario.Email);
        }

        public async Task ExcluirReceitaAsync(Guid receitaId)
        {
            var usuario = await _userContext.GetAuthenticatedUserAsync();
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            await _receitasRepository.ExcluirReceitaAsync(receitaId, usuario.Email);
        }
    }
}
