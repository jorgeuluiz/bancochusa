using BancoChuSA.Application.UseCases.Transferencias.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BancoChuSA.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GeExcel(
        [FromServices] IGenerateTransferenciasReportExcelUseCase useCase,
        [FromHeader] DateTime month)
    {
        byte[] file = await useCase.Execute(month);

        if (file.Length > 0)
        {   
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
        }

        return NoContent();

    }
}
