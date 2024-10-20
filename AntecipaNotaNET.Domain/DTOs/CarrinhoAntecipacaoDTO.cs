namespace AntecipaNotaNET.Domain.DTOs;

public record CarrinhoAntecipacaoDTO(
    string nome,
    string cnpj,
    decimal limite,
    List<ValorNotaDTO> notasFiscais,
    decimal totalLiquido,
    decimal totalBruto
);
