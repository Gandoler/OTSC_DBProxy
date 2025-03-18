using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample;

public class SuccessfulSubscriptionExample : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            message = "Подписка успешно оформлена"
        };
    }
}
public class FailedSubscriptionExample : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            message = "Ошибка при оформлении подписки"
        };
    }
}