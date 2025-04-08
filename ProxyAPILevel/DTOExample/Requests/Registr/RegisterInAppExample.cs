// <copyright file="RegisterInAppExample.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILeval.DTOExample;

using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

public class RegisterInAppExample : IExamplesProvider<RegisterDto>
{
    public RegisterDto GetExamples()
    {
        return new RegisterDto
        {
            Login = "admin",
            Password = "password",
            Email = string.Empty,
        };
    }
}
