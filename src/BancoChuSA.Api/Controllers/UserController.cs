using BancoChuSA.Application.UseCases.Users.Cadastrar;
using BancoChuSA.Communication.Requests;
using BancoChuSA.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BancoChuSA.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseCadastrarUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Cadastrar(
        [FromServices] ICadastrarUserUseCase useCase,
        [FromBody] RequestCadastrarUserJson request)
    {

        var response = await useCase.Execute(request);

        return Created(string.Empty, response);

    }
}
