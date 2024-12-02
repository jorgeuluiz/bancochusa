namespace BancoChuSA.Application.UseCases.Transferencias.Reports;

public interface IGenerateTransferenciasReportExcelUseCase
{
    Task<byte[]> Execute(DateTime month);
}
