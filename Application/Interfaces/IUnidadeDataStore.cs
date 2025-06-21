using PersonalizacaoImobiliaria.Application.DTO;
using PersonalizacaoImobiliaria.Domain.Entities;
using PersonalizacaoImobiliaria.Domain.Enums;

namespace PersonalizacaoImobiliaria.Application.Interfaces;

public interface IUnidadeDataStore
{
    Task<Guid> CadastrarUnidadeVendida(Unidade unidadeVendida);
    Task<Guid> CadastrarPersonalizacao(Personalizacao personalizacao);
    Task<List<PersonalizacoesDTO>> ListarPersonalizacoes(ETipoPersonalizacao? tipoPersonalizacao);
    Task<Unidade?> GetUnidadePorId(Guid idUnidade);
    Task<Guid?> GetPersonalizacaoPorNome(string nomeUnidade);
    Task<List<Personalizacao>?> ListarPersonalizacoesPorId(List<Guid> idPersonalizacoes);
}
