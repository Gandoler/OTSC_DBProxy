using AutoMapper;

namespace UseCases;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Infrastructure.Models.User, User>().ReverseMap();
        CreateMap<Infrastructure.Models.Pozdrik, Pozdrik>().ReverseMap();
        CreateMap<Infrastructure.Models.FriendList, FriendList>().ReverseMap();
        CreateMap<Infrastructure.Models.MailComprehension, MailComprehension>().ReverseMap();
        CreateMap<Infrastructure.Models.TgComprehension, TgComprehension>().ReverseMap();
    }
}