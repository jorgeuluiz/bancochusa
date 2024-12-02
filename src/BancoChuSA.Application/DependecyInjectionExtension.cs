using BancoChuSA.Application.AutoMapper;
using BancoChuSA.Application.UseCases.Contas.Cadastrar;
using BancoChuSA.Application.UseCases.Contas.GetAll;
using BancoChuSA.Application.UseCases.Login.DoLogin;
using BancoChuSA.Application.UseCases.Transferencias.Cadastrar;
using BancoChuSA.Application.UseCases.Transferencias.GetAll;
using BancoChuSA.Application.UseCases.Transferencias.Reports;
using BancoChuSA.Application.UseCases.Users.Cadastrar;
using Microsoft.Extensions.DependencyInjection;

namespace BancoChuSA.Application;

public static class DependecyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ICadastrarContaUseCase, CadastrarContaUseCase>();
        services.AddScoped<ICadastrarTransferenciaUseCase, CadastrarTransferenciaUseCase>();
        services.AddScoped<IGetAllContaUseCase, GetAllContaUseCase>();
        services.AddScoped<IGetAllTransferenciaUseCase, GetAllTransferenciaUseCase>();
        services.AddScoped<IGenerateTransferenciasReportExcelUseCase, GenerateTransferenciasReportExcelUseCase>();
        services.AddScoped<ICadastrarUserUseCase, CadastrarUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
    }


}
