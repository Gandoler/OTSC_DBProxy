// <copyright file="CheckExistByMailExample.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILeval.DTOExample;

using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

public class CheckExistByMailExample : IExamplesProvider<CheckExistDto>
{
    public CheckExistDto GetExamples()
    {
        return new CheckExistDto
        {
            Email = "example@example.com",
        };
    }
}
