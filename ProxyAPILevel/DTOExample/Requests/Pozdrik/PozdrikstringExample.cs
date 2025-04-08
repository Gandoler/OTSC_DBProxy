// <copyright file="PozdrikstringExample.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILeval.DTOExample;

using Domain.DTO.DTO.Pozdr;
using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

public class PozdrikstringExample : IExamplesProvider<PozdrStringDTO>
{
    public PozdrStringDTO GetExamples()
    {
        return new PozdrStringDTO
        {
            PozdrikId = 2,
            Pozdr = "efefaiwjijpaifjkpiafjoawfpoawfkpaof",
        };
    }
}
