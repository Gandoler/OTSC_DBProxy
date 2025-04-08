// <copyright file="DeleteFriendDtoExample.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILeval.DTOExample;

using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

public class DeleteFriendDtoExample : IExamplesProvider<DeleteFriendDto>
{
    public DeleteFriendDto GetExamples()
    {
        return new DeleteFriendDto
        {
            AppId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
            FriendUsername = "best_friend123",
        };
    }
}
