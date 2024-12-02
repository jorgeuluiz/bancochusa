using AutoMapper;
using BancoChuSA.Communication.Responses;
using BancoChuSA.Domain.Repositories.Contas;

namespace BancoChuSA.Application.UseCases.Contas.GetAll;

public class GetAllContaUseCase : IGetAllContaUseCase
{
    private readonly IContaReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllContaUseCase(IContaReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseContaJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseContaJson
        {
            Contas = _mapper.Map<List<ResponseShortContaJson>>(result)
        };

    }
}
