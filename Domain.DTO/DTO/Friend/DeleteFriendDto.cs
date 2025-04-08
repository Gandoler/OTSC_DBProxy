// <copyright file="DeleteFriendDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Entities.Templates;

public class DeleteFriendDto
{
    public Guid AppId { get; set; }

    public string FriendUsername { get; set; } = null!;
}
