// <copyright file="LoginDtoExample.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILeval.DTOExample;

using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

public class LoginDtoExample : IExamplesProvider<LoginDto>
{
    public LoginDto GetExamples()
    {
        return new LoginDto
        {
            Login = "user1",
            Password = "password1",
        };
    }
}
