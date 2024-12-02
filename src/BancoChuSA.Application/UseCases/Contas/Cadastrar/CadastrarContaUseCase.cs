using AutoMapper;
using BancoChuSA.Communication.Requests;
using BancoChuSA.Communication.Responses;
using BancoChuSA.Domain.Entities;
using BancoChuSA.Domain.Repositories;
using BancoChuSA.Domain.Repositories.Contas;
using BancoChuSA.Domain.Repositories.User;
using BancoChuSA.Domain.Services.LoggedUser;
using BancoChuSA.Exception;
using BancoChuSA.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace BancoChuSA.Application.UseCases.Contas.Cadastrar;

public class CadastrarContaUseCase : ICadastrarContaUseCase
{
    private readonly IContaWriteOnlyRepository _repository;
    private readonly IContaReadOnlyRepository _contaReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;


    public CadastrarContaUseCase(IContaWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IContaReadOnlyRepository contaReadOnlyRepository, ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contaReadOnlyRepository = contaReadOnlyRepository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseCadastrarContaJson> Execute(RequestCadastrarContaJson request)
    {
        await Validate(request);

        var loggerUser = await _loggedUser.Get();

        var entity = _mapper.Map<Conta>(request);
        entity.UserId = loggerUser.Id;

        await _repository.Add(entity);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseCadastrarContaJson>(entity);
    }

    private async Task Validate(RequestCadastrarContaJson request)
    {
        var validator = new ContaValidator();

        var result = validator.Validate(request);

        var contaExist = await _contaReadOnlyRepository.ExistNumeroConta(request.NumeroConta);
        if (contaExist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.CONTA_REGISTERED));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }               

    }

}
