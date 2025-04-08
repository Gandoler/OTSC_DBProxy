// <copyright file="LoginResponseExample.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILeval.DTOExample;

using Swashbuckle.AspNetCore.Filters;

public class LoginResponseExample : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            exists = true,
        };
    }
}
