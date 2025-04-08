// <copyright file="SuccessfulSubscriptionExample.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILeval.DTOExample;

using Swashbuckle.AspNetCore.Filters;

public class SuccessfulSubscriptionExample : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            message = "Подписка успешно оформлена",
        };
    }
}

public class FailedSubscriptionExample : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            message = "Ошибка при оформлении подписки",
        };
    }
}
