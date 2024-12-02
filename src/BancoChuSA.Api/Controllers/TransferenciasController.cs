using BancoChuSA.Application.UseCases.Transferencias.Cadastrar;
using BancoChuSA.Application.UseCases.Transferencias.GetAll;
using BancoChuSA.Communication.Requests;
using BancoChuSA.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BancoChuSA.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TransferenciasController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseCadastrarTransferenciaJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]    
    public async Task<IActionResult> Cadastrar(
        [FromServices] ICadastrarTransferenciaUseCase useCase,
        [FromBody] RequestCadastrarTransferenciaJson request)
    {       

        var response = await useCase.Execute(request);

        return Created(string.Empty, response);

    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseTransferenciaJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize]
    public async Task<IActionResult> GetAllTransferencias(
        [FromServices] IGetAllTransferenciaUseCase useCase)
    {

        var response = await useCase.Execute();

        if (response.Transferencias.Count != 0)
        {
            return Ok(response);
        }

        return NoContent();

    }
}
