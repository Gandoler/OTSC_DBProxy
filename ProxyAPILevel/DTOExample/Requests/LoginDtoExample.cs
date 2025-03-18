using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample;

public class LoginDtoExample: IExamplesProvider<LoginDto>
{
    public LoginDto GetExamples()
    {
        return new LoginDto
        {
            Login = "user1",
            Password = "password1"
        };
    }
}