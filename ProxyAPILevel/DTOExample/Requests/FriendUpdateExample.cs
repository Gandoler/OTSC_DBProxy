using Domain.DTO.DTO.Friend;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample;

public class FriendUpdateExample : IExamplesProvider<FriendDto>
{
    public FriendDto GetExamples()
    {
        return new FriendDto
        {
            AppId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
            FriendUsername = "best_friend123",
            FriendName = "Иван Гришин",
            DateBirth = new DateOnly(1990, 5, 20)
        };
    }
}