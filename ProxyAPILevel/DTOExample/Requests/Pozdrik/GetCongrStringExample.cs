// <copyright file="GetCongrStringExample.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILeval.DTOExample;

using Domain.DTO.DTO.Pozdr;
using Swashbuckle.AspNetCore.Filters;

public class GetCongrStringExample : IExamplesProvider<PozdrikIdDto>
{
    public PozdrikIdDto GetExamples()
    {
        return new PozdrikIdDto
        {
            PozdrikId = 6,
        };
    }
}
