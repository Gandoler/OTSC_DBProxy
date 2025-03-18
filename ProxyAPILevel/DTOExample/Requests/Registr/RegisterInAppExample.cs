using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample;

public class RegisterInAppExample: IExamplesProvider<RegisterDto>
{
    public RegisterDto GetExamples()
    {
        return new RegisterDto
        {
            Login = "admin",
            Password = "password",
            Email = string.Empty,
        };
    }
}