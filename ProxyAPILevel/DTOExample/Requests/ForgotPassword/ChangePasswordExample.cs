// <copyright file="ChangePasswordExample.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILeval.DTOExample.ForgotPassword;

using Entities.Templates;
using ProxyAPILevel;
using Swashbuckle.AspNetCore.Filters;

public class ChangePasswordExample : IExamplesProvider<LoginDto>
{
    public LoginDto GetExamples()
    {
        return new LoginDto
        {
            Password = "faaweafafw234",
            Login = "admin",
        };
    }
}
