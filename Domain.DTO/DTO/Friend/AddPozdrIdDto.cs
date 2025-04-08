// <copyright file="AddPozdrIdDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.DTO.DTO.Friend;

public class AddPozdrIdDto
{
    public int PozdrikId { get; set; }

    public string FriendUsername { get; set; } = string.Empty;

    public Guid AppId { get; set; }
}
