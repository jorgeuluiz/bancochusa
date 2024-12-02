using AutoMapper;
using BancoChuSA.Communication.Responses;
using BancoChuSA.Domain.Repositories.Transferencias;

namespace BancoChuSA.Application.UseCases.Transferencias.GetAll;

public class GetAllTransferenciaUseCase : IGetAllTransferenciaUseCase
{
    private readonly ITransferenciaReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllTransferenciaUseCase(ITransferenciaReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTransferenciaJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseTransferenciaJson
        {
            Transferencias = _mapper.Map<List<ResponseShortTransferenciaJson>>(result)
        };
    }
}
