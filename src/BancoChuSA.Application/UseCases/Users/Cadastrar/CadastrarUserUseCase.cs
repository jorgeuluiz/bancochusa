using AutoMapper;
using BancoChuSA.Communication.Requests;
using BancoChuSA.Communication.Responses;
using BancoChuSA.Domain.Repositories;
using BancoChuSA.Domain.Repositories.User;
using BancoChuSA.Domain.Security.Cryptography;
using BancoChuSA.Domain.Security.Tokens;
using BancoChuSA.Exception;
using BancoChuSA.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace BancoChuSA.Application.UseCases.Users.Cadastrar;

public class CadastrarUserUseCase : ICadastrarUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncrypter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _tokenGenerator;


    public CadastrarUserUseCase(IMapper mapper, IPasswordEncrypter passwordEncripter, IUserReadOnlyRepository userReadOnlyRepository, IUnitOfWork unitOfWork, IUserWriteOnlyRepository userWriteOnlyRepository, IAccessTokenGenerator tokenGenerator)
    {
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResponseCadastrarUserJson>Execute(RequestCadastrarUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _userWriteOnlyRepository.Add(user);
        await _unitOfWork.Commit();

        return new ResponseCadastrarUserJson
        {
            Nome = user.Nome,
            Token = _tokenGenerator.Generate(user)
        };
    }

    private async Task Validate(RequestCadastrarUserJson request)
    {
        var result = new CadastrarUserValidator().Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
        if (emailExist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_REGISTERED));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
