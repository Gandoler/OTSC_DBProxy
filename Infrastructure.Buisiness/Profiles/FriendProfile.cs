// <copyright file="FriendProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UseCases.Profiles;

using AutoMapper;
using Domain.DTO.DTO.Friend;
using Domain.Models;
using Entities.Templates;

public class FriendProfile : Profile
{
    public FriendProfile()
    {
        this.CreateMap<FriendList, FriendDto>();
    }
}
