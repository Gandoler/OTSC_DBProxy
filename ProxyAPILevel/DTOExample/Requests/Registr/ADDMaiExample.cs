using Domain.DTO.DTO.MailComp;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample;

public class ADDMaiExample: IExamplesProvider<ADDMailDto>
{
    public ADDMailDto GetExamples()
    {
        return new ADDMailDto
        {
            Appid = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
            Email = "example@example.com",
        };
    }
}