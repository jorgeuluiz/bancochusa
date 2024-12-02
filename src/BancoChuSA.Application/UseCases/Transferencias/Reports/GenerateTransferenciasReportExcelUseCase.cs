using BancoChuSA.Domain.Reports;
using BancoChuSA.Domain.Repositories.Transferencias;
using ClosedXML.Excel;

namespace BancoChuSA.Application.UseCases.Transferencias.Reports;

public class GenerateTransferenciasReportExcelUseCase : IGenerateTransferenciasReportExcelUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly ITransferenciaReadOnlyRepository _repository;

    public GenerateTransferenciasReportExcelUseCase(ITransferenciaReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateTime month)
    {
        var transferencias = await _repository.FilterByMonth(month);

        if (transferencias.Count == 0)
        {
            return Array.Empty<byte>();
        }

        using var workbook = new XLWorkbook();

        workbook.Author = "Jorge";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        var raw = 2;

        foreach (var transferencia in transferencias)
        {
            worksheet.Cell($"A{raw}").Value = transferencia.NumeroConta;
            worksheet.Cell($"B{raw}").Value = transferencia.Data;
            worksheet.Cell($"C{raw}").Value = transferencia.Valor;
            worksheet.Cell($"C{raw}").Style.NumberFormat.Format = $"{CURRENCY_SYMBOL} #,##0.00";

            raw++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();

    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.NUMERO_CONTA;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATA;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.VALOR;

        worksheet.Cells("A1:C1").Style.Font.Bold = true;

        worksheet.Cells("A1:C1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
    }
}
