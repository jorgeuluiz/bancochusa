using BancoChuSA.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests;

public class RequestCadastrarContaJsonBuilder
{
    public static RequestCadastrarContaJson Build()
    {       
        return new Faker<RequestCadastrarContaJson>()
            //.RuleFor(r => r.Nome, faker => faker.Name.FirstName())
            .RuleFor(r => r.NumeroConta, faker => faker.Finance.Account())
            .RuleFor(r => r.Saldo, faker => faker.Finance.Amount(min: 1, max: 1000));

    }
}
