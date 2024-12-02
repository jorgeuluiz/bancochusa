using BancoChuSA.Domain.Repositories;
using BancoChuSA.Domain.Repositories.Contas;
using BancoChuSA.Domain.Repositories.Transferencias;
using BancoChuSA.Domain.Repositories.User;
using BancoChuSA.Domain.Security.Cryptography;
using BancoChuSA.Domain.Security.Tokens;
using BancoChuSA.Domain.Services.LoggedUser;
using BancoChuSA.Infrastructure.DataAccess;
using BancoChuSA.Infrastructure.DataAccess.Repositories;
using BancoChuSA.Infrastructure.Security.Tokens;
using BancoChuSA.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BancoChuSA.Infrastructure;

public static class DependecyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
       
        services.AddScoped<IPasswordEncrypter, Security.Cryptography.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();

        AddDbContext(services, configuration);
        AddToken(services, configuration);
        AddRepositories(services);

    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var sigingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, sigingKey!));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IContaWriteOnlyRepository, ContaRepository>();
        services.AddScoped<IContaReadOnlyRepository, ContaRepository>();
        services.AddScoped<ITransferenciaWriteOnlyRepository, TransferenciaRepository>();
        services.AddScoped<ITransferenciaReadOnlyRepository, TransferenciaRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();

    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        //var version = new Version(9, 0, 1);
        //var serverVersion = new MySqlServerVersion(version);

        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<BancoChuSADbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
