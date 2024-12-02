using BancoChuSA.Application.UseCases.Contas.Cadastrar;
using BancoChuSA.Application.UseCases.Contas.GetAll;
using BancoChuSA.Communication.Requests;
using BancoChuSA.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BancoChuSA.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ContasController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCadastrarContaJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Cadastrar(
        [FromServices] ICadastrarContaUseCase useCase,
        [FromBody] RequestCadastrarContaJson request)
    {       

        var response = await useCase.Execute(request);

        return Created(string.Empty, response);

    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseContaJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllContas(
        [FromServices] IGetAllContaUseCase useCase)
    {

        var response = await useCase.Execute();

        if (response.Contas.Count != 0)
        {
            return Ok(response);
        }

        return NoContent();

    }
}
