// <copyright file="GetPozdIdInTgExample.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProxyAPILeval.DTOExample;

using Domain.DTO.DTO.Friend;
using Swashbuckle.AspNetCore.Filters;

public class GetPozdIdInTgExample : IExamplesProvider<FriendDto>
{
    public FriendDto GetExamples()
    {
        return new FriendDto
        {
            AppId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
            FriendUsername = "best_friend123",
            FriendName = string.Empty,
        };
    }
}
