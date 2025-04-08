// <copyright file="ChangePasswordDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Entities.Templates;

public class ChangePasswordDto
{
    public Guid AppId { get; set; }

    public string NewPassword { get; set; } = null!;
}
