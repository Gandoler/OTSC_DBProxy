// <copyright file="ADDMaiExample.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILeval.DTOExample;

using Domain.DTO.DTO.MailComp;
using Swashbuckle.AspNetCore.Filters;

public class ADDMaiExample : IExamplesProvider<ADDMailDto>
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
