// <copyright file="AddFriendWithWishDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.DTO.DTO.Friend;

using Entities.Templates;

public class AddFriendWithWishDTO
{
    public FriendDto Friend { get; set; }

    public AddIntAndPozhDto Pozh { get; set; }
}
