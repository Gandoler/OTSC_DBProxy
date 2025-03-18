using Domain.DTO.DTO.Friend;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample;

public class FriendDtoExample : IExamplesProvider<FriendDto>
{
    public FriendDto GetExamples()
    {
        return new FriendDto
        {
            AppId = Guid.Parse("123e4567-e89b-12d3-a456-426614174000"),
            FriendUsername = "best_friend123",
            FriendName = "Иван Иванов",
            DateBirth = new DateOnly(1990, 5, 20)
        };
    }
}