using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample;

public class LoginResponseExample: IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            exists = true
        };
    }
}