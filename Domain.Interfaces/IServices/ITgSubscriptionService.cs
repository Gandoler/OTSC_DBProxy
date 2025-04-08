// <copyright file="ITgSubscriptionService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Interfaces.IServices;

using Domain.Models;
using Entities.Templates;

public interface ITgSubscriptionService
{
    Task<bool> SubscribeAsync(RegisterTgDto dto);
}
