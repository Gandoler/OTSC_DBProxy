using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample;

public class RegisterTgDtoExample : IExamplesProvider<RegisterTgDto>
{
    public RegisterTgDto GetExamples()
    {
        return new RegisterTgDto
        {
            AppId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
            TgId = 1234567890
        };
    }
}