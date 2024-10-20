namespace AntecipaNotaNET.Domain.Exceptions;

public class EmpresaValorLimiteExcedidoException(decimal limiteEmpresa, decimal limiteExcedido)
    : Exception(
        $"O valor total das notas fiscais no carrinho excede o limite de crédito da empresa. Limite atual: R$ {limiteEmpresa} | Limite excedido: R$ {limiteExcedido}"
    ) { }
