using Microsoft.EntityFrameworkCore;
using PersonalizacaoImobiliaria.Application.DTO;
using PersonalizacaoImobiliaria.Application.Interfaces;
using PersonalizacaoImobiliaria.Domain.Entities;
using PersonalizacaoImobiliaria.Domain.Enums;
using PersonalizacaoImobiliaria.Infrastructure.Database.Context;

namespace PersonalizacaoImobiliaria.Infrastructure.Database.DataStore;

public class UnidadeDataStore : IUnidadeDataStore
{
    private readonly PersonalizacaoImobiliariaContext _personalizacaoImobiliariaContext;

    public UnidadeDataStore(PersonalizacaoImobiliariaContext personalizacaoImobiliariaContext)
    {
        _personalizacaoImobiliariaContext = personalizacaoImobiliariaContext;
    }

    public async Task<Guid> CadastrarUnidadeVendida(Unidade unidadeVendida)
    {
        await _personalizacaoImobiliariaContext.AddAsync(unidadeVendida);

        await _personalizacaoImobiliariaContext.SaveChangesAsync();

        return unidadeVendida.Id;
    }

    public async Task<Guid> CadastrarPersonalizacao(Personalizacao personalizacao)
    {
        await _personalizacaoImobiliariaContext.AddAsync(personalizacao);

        await _personalizacaoImobiliariaContext.SaveChangesAsync();

        return personalizacao.Id;
    }


    public async Task<List<PersonalizacoesDTO>> ListarPersonalizacoes(ETipoPersonalizacao? tipoPersonalizacao)
    {
        var queryPersonalizacoes = _personalizacaoImobiliariaContext.Personalizacao.AsQueryable();

        if (tipoPersonalizacao.HasValue)
            queryPersonalizacoes = queryPersonalizacoes.Where(p => p.Tipo == tipoPersonalizacao);

        return await queryPersonalizacoes.Select(p => new PersonalizacoesDTO
        {
            Nome = p.Nome,
            Descricao = p.Descricao,
            Tipo = p.Tipo,
            Valor = p.Valor
        }).ToListAsync(); 
    }

    public async Task<Unidade?> GetUnidadePorId(Guid idUnidade)
    {
        return await _personalizacaoImobiliariaContext.Unidade.FirstOrDefaultAsync(u => u.Id == idUnidade);
    }

    public async Task<List<Personalizacao>?> ListarPersonalizacoesPorId(List<Guid> idPersonalizacoes)
    {
       return await _personalizacaoImobiliariaContext.Personalizacao.Where(p => idPersonalizacoes.Contains(p.Id)).ToListAsync();
    }

    public async Task<Guid?> GetPersonalizacaoPorNome(string nomeUnidade)
    {
        return await _personalizacaoImobiliariaContext.Personalizacao.Where(p => p.Nome == nomeUnidade).Select(p => p.Id).FirstOrDefaultAsync();
    }
}
