using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample;

public class DeleteFriendDtoExample : IExamplesProvider<DeleteFriendDto>
{
    public DeleteFriendDto GetExamples()
    {
        return new DeleteFriendDto
        {
            AppId = Guid.Parse("123e4567-e89b-12d3-a456-426614174000"),
            FriendUsername = "best_friend123"
        };
    }
}