using BancoChuSA.Application.UseCases.Login.DoLogin;
using BancoChuSA.Communication.Requests;
using BancoChuSA.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BancoChuSA.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseCadastrarUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromServices] IDoLoginUseCase useCase,
        [FromBody] RequestLoginJson request)
    {

        var response = await useCase.Execute(request);

        return Ok(response);

    }
}
