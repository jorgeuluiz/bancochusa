using AutoMapper;
using BancoChuSA.Communication.Requests;
using BancoChuSA.Communication.Responses;
using BancoChuSA.Domain.Entities;
using BancoChuSA.Domain.Repositories;
using BancoChuSA.Domain.Repositories.Contas;
using BancoChuSA.Domain.Repositories.Transferencias;
using BancoChuSA.Exception;
using BancoChuSA.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace BancoChuSA.Application.UseCases.Transferencias.Cadastrar;

public class CadastrarTransferenciaUseCase : ICadastrarTransferenciaUseCase
{
    private readonly ITransferenciaWriteOnlyRepository _repository;
    private readonly IContaReadOnlyRepository _contaReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IContaWriteOnlyRepository _contaWriteOnlyRepository;

    public CadastrarTransferenciaUseCase(
        ITransferenciaWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IContaReadOnlyRepository contaReadOnlyRepository,
        IContaWriteOnlyRepository contaWriteOnlyRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contaReadOnlyRepository = contaReadOnlyRepository;
        _contaWriteOnlyRepository = contaWriteOnlyRepository;
    }

    public async Task<ResponseCadastrarTransferenciaJson> Execute(RequestCadastrarTransferenciaJson request)
    {
        await Validate(request);

        var entity = _mapper.Map<Transferencia>(request);

        await _repository.Add(entity);
        await UpdateContaSaldo(entity);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseCadastrarTransferenciaJson>(entity);
    }

    private async Task UpdateContaSaldo(Transferencia entity)
    {
        if (entity.Data.Day == DateTime.UtcNow.Day && entity.Data.Month == DateTime.UtcNow.Month)
        {
            var conta = await _contaReadOnlyRepository.Get(entity.NumeroConta);

            conta.Saldo += entity.Valor;

            await _contaWriteOnlyRepository.Update(conta);
        }
    }

    private async Task Validate(RequestCadastrarTransferenciaJson request)
    {
        var validator = new TransferenciaValidator();

        var result = validator.Validate(request);

        var contaExist = await _contaReadOnlyRepository.ExistNumeroConta(request.NumeroConta);
        if (!contaExist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.CONTA_NOT_REGISTERED));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }

    }
}
