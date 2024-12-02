namespace BancoChuSA.Application.UseCases.Transferencias;

public static class Feriados
{
    public static List<DateTime> GetFeriados()
    {
        // Lista de feriados para o ano (exemplo)
        return new List<DateTime>
        {
            new DateTime(DateTime.Now.Year, 1, 1),  // Confraternização mundial
            new DateTime(DateTime.Now.Year, 2, 21), // Carnaval
            new DateTime(DateTime.Now.Year, 4, 7),  // Sexta-feira Santa
            new DateTime(DateTime.Now.Year, 4, 9),  // Páscoa
            new DateTime(DateTime.Now.Year, 4, 21),  // Tiradentes
            new DateTime(DateTime.Now.Year, 5, 1),  // Dia do trabalho
            new DateTime(DateTime.Now.Year, 6, 8),  // Corpus Christi
            new DateTime(DateTime.Now.Year, 9, 7),  // Independência do Brasil
            new DateTime(DateTime.Now.Year, 10, 12),  // Nossa Senhora Aparecida
            new DateTime(DateTime.Now.Year, 11, 2),  // Finados
            new DateTime(DateTime.Now.Year, 11, 15), // Proclamação da República
            new DateTime(DateTime.Now.Year, 12, 25) // Natal
        };
    }
}