using AntecipaNotaNET.Domain.DTOs;
using AntecipaNotaNET.Domain.Entities;
using AntecipaNotaNET.Domain.Exceptions;
using AntecipaNotaNET.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AntecipaNotaNET.Presentation.Controllers;

[ApiController]
[Route("api/recebivel")]
public class RecebivelController(IServiceRecebivel serviceRecebivel) : ControllerBase
{
    private readonly IServiceRecebivel _serviceRecebivel = serviceRecebivel;

    [HttpPost("empresa")]
    [ProducesResponseType(typeof(Empresa), StatusCodes.Status200OK)]
    public async Task<IActionResult> GravarEmpresa([FromBody] EmpresaDTO empresaDto) =>
        Ok(await _serviceRecebivel.AdicionarNovaEmpresa(empresaDto));

    [HttpPost("nota")]
    [ProducesResponseType(typeof(NotaFiscal), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GravarNotaFiscal([FromBody] NotaFiscalDTO nota)
    {
        try
        {
            return Ok(await _serviceRecebivel.AdicionarNovaNotaFiscal(nota));
        }
        catch (EmpresaInexistenteException ex)
        {
            return StatusCode(400, new { Mensagem = ex.Message });
        }
        catch (DataNotaInvalidaException ex)
        {
            return StatusCode(400, new { Mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Mensagem = ex.Message });
        }
    }

    [HttpGet("carinho/adicionar/{nota:int}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AdicionarNota(int nota)
    {
        try
        {
            await _serviceRecebivel.AdicionarNotaAoCarrinho(nota);

            return NoContent();
        }
        catch (NotaCarrinhoAdicionadaException ex)
        {
            return StatusCode(400, new { Mensagem = ex.Message });
        }
        catch (NotaInvalidaException ex)
        {
            return StatusCode(400, new { Mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Mensagem = ex.Message });
        }
    }

    [HttpGet("carinho/remover/{nota:int}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoverNota(int nota)
    {
        try
        {
            await _serviceRecebivel.RemoverNotaDoCarrinho(nota);

            return NoContent();
        }
        catch (NotaCarrinhoInexistenteException ex)
        {
            return StatusCode(400, new { Mensagem = ex.Message });
        }
        catch (NotaInvalidaException ex)
        {
            return StatusCode(400, new { Mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Mensagem = ex.Message });
        }
    }

    [HttpGet("carinho/antecipacao")]
    [ProducesResponseType(typeof(CarrinhoAntecipacaoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CalcularAntecipacao([FromQuery] string cnpj)
    {
        try
        {
            return Ok(await _serviceRecebivel.CalcularAntecipacaoCarrinho(cnpj));
        }
        catch (EmpresaInexistenteException ex)
        {
            return StatusCode(400, new { Mensagem = ex.Message });
        }
        catch (EmpresaValorLimiteExcedidoException ex)
        {
            return StatusCode(400, new { Mensagem = ex.Message });
        }
        catch (Exception)
        {
            throw;
        }
    }
}
