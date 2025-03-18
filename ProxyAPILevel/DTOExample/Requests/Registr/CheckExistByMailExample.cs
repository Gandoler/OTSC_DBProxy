using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample;

public class CheckExistByMailExample: IExamplesProvider<CheckExistDto>
{
    public CheckExistDto GetExamples()
    {
        return new CheckExistDto
        {
            Email = "example@example.com",
        };
    }
}