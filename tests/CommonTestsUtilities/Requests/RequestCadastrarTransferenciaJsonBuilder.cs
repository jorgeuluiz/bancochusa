using BancoChuSA.Communication.Requests;
using Bogus;
using Bogus.DataSets;

namespace CommonTestUtilities.Requests;

public class RequestCadastrarTransferenciaJsonBuilder
{
    public static RequestCadastrarTransferenciaJson Build()
    {
        return new Faker<RequestCadastrarTransferenciaJson>()
            .RuleFor(r => r.Data, faker => GerarDataFuturaUtil(faker))
            .RuleFor(r => r.NumeroConta, faker => faker.Finance.Account())
            .RuleFor(r => r.Valor, faker => faker.Finance.Amount(min: 1, max: 1000));

    }

    public static DateTime GerarDataFuturaUtil(Faker f)
    {    
        var dataFutura = f.Date.Future(365);

        while (dataFutura.DayOfWeek == DayOfWeek.Saturday || dataFutura.DayOfWeek == DayOfWeek.Sunday)
        {            
            if (dataFutura.DayOfWeek == DayOfWeek.Saturday)
                dataFutura = dataFutura.AddDays(2);            
            else if (dataFutura.DayOfWeek == DayOfWeek.Sunday)
                dataFutura = dataFutura.AddDays(1);
        }

        return dataFutura;
    }
}
