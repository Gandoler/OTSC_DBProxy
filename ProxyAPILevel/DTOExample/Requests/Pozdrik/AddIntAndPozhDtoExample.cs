// <copyright file="AddIntAndPozhDtoExample.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILeval.DTOExample;

using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

public class AddIntAndPozhDtoExample : IExamplesProvider<AddIntAndPozhDto>
{
    public AddIntAndPozhDto GetExamples()
    {
        return new AddIntAndPozhDto
        {
            IdPozdr = 1,
            Interests = "Программирование, Чтение книг",
            Pozhelania = "Счастья и успехов!",
        };
    }
}
