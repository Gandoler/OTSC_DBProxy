using Domain.DTO.DTO.Friend;
using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample;

public class SetPozdrIdExample: IExamplesProvider<AddPozdrIdDto>
{
    public AddPozdrIdDto GetExamples()
    {
        return new AddPozdrIdDto
        {
            AppId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
            FriendUsername = "best_friend123",
            PozdrikId = 6
        };
    }
}