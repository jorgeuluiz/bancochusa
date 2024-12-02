using BancoChuSA.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests;

public class RequestCadastrarUserJsonBuilder
{
    public static RequestCadastrarUserJson Build()
    {
        return new Faker<RequestCadastrarUserJson>()
            .RuleFor(user => user.Nome, faker => faker.Person.FirstName)
            .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Nome))
            .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "!Aa1"));
    }
}
