// <copyright file="FriendList.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Models;

using Domain.Models;

public partial class FriendList
{
    public Guid Appid { get; set; }

    public string FriendUsername { get; set; } = null!;

    public string FriendName { get; set; } = null!;

    public DateOnly DateBirth { get; set; }

    public int? IdPozdr { get; set; }

    public virtual User App { get; set; } = null!;

    public virtual Pozdrik? IdPozdrNavigation { get; set; }
}
