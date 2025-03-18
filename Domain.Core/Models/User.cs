using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class User
{
    public Guid Appid { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<FriendList> FriendLists { get; set; } = new List<FriendList>();

    public virtual ICollection<MailComprehension> MailComprehensions { get; set; } = new List<MailComprehension>();

    public virtual ICollection<TgComprehension> TgComprehensions { get; set; } = new List<TgComprehension>();
}