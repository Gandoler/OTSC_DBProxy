using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample;

public class AddIntAndPozhDtoExample : IExamplesProvider<AddIntAndPozhDto>
{
    public AddIntAndPozhDto GetExamples()
    {
        return new AddIntAndPozhDto
        {
            IdPozdr = 42,
            Interests = "Программирование, Чтение книг",
            Pozhelania = "Счастья и успехов!"
        };
    }
}