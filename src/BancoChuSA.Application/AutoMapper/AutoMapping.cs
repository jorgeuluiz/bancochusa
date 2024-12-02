using AutoMapper;
using BancoChuSA.Communication.Requests;
using BancoChuSA.Communication.Responses;
using BancoChuSA.Domain.Entities;

namespace BancoChuSA.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestCadastrarContaJson, Conta>();
        CreateMap<RequestCadastrarTransferenciaJson, Transferencia>();
        CreateMap<RequestCadastrarUserJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());
    }

    private void EntityToResponse()
    {
        CreateMap<Conta, ResponseCadastrarContaJson>();
        CreateMap<Conta, ResponseShortContaJson>();
        CreateMap<Transferencia, ResponseShortTransferenciaJson>();
        CreateMap<Transferencia, ResponseCadastrarTransferenciaJson>();
    }
}
