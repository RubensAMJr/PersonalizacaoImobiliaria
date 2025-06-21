using Microsoft.EntityFrameworkCore;
using PersonalizacaoImobiliaria.Application.DTO;
using PersonalizacaoImobiliaria.Application.Interfaces;
using PersonalizacaoImobiliaria.Domain.Entities;
using PersonalizacaoImobiliaria.Domain.Enums;
using PersonalizacaoImobiliaria.Infrastructure.Database.Context;
using System;

namespace PersonalizacaoImobiliaria.Infrastructure.Database.DataStore
{
    public class SolicitacaoDataStore : ISolicitacaoDataStore
    {
        private readonly PersonalizacaoImobiliariaContext _personalizacaoImobiliariaContext;

        public SolicitacaoDataStore(PersonalizacaoImobiliariaContext personalizacaoImobiliariaContext)
        {
            _personalizacaoImobiliariaContext = personalizacaoImobiliariaContext;
        }

        public async Task<Guid> CadastrarSolicitacao(Solicitacao solicitacao)
        {
            await _personalizacaoImobiliariaContext.Solicitacao.AddAsync(solicitacao);

            await _personalizacaoImobiliariaContext.SaveChangesAsync();

            return solicitacao.Id;
        }

        public async Task<List<SolicitacoesDTO>> ListarSolicitacoes(ETipoOrdenacaoSolicitacoes? tipoOrdenacaoSolicitacoes)
        {
            var solicitacoes = _personalizacaoImobiliariaContext.Solicitacao.Include(u => u.Unidade).Include(p => p.Personalizacoes).AsQueryable();

            if (tipoOrdenacaoSolicitacoes.HasValue)
                solicitacoes = tipoOrdenacaoSolicitacoes == ETipoOrdenacaoSolicitacoes.Data ? solicitacoes.OrderBy(s => s.DataCriacao) : solicitacoes.OrderByDescending(s => s.ValorTotal);

            return await solicitacoes.Select(s => new SolicitacoesDTO
            {
                UnidadeId = s.Unidade.Id,
                NomeUnidade = s.Unidade.Nome,
                NomeCliente = s.Unidade.NomeCliente,
                Personalizacoes = s.Personalizacoes,
                Status = s.Status,
                ValorTotal = s.ValorTotal,
                DataSolicitacao = s.DataCriacao
            }).ToListAsync();
        }

        public async Task<string?> GetSolicitacaoByUnidade(Guid unidadeId)
        {
            return await _personalizacaoImobiliariaContext.Solicitacao.Where(s => s.Unidade.Id == unidadeId).Select(u => u.Unidade.Nome).FirstOrDefaultAsync();
        }
    }
}