using AutoMapper;
using Domain.DTO.DTO.Friend;
using Domain.Models;
using Entities.Templates;

namespace UseCases.Profiles;

public class FriendProfile: Profile
{
    public FriendProfile()
    {
        CreateMap<FriendList, FriendDto>();

    }
}