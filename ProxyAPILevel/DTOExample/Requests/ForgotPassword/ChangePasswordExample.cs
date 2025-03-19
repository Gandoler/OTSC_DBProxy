using Entities.Templates;
using ProxyAPILevel;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample.ForgotPassword;

public class ChangePasswordExample : IExamplesProvider<LoginDto>
{
    public LoginDto GetExamples()
    {
        return new LoginDto
        {
           Password = "faaweafafw234",
           Login = "admin"
        };
    }
}