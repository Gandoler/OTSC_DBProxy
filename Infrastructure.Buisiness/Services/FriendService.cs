using AutoMapper;
using Domain.DTO.DTO.Friend;
using Domain.DTO.DTO.Pozdr;
using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

namespace UseCases.Services;

public class FriendService:IFriendService
{
    private readonly IFriendRepository _friendRepository;
    private readonly IPozdrikRepository _pozdrikRepository;
    private readonly IMapper _mapper;

    public FriendService(IFriendRepository friendRepository, IPozdrikRepository pozdrikRepository, IMapper mapper)
    {
        _mapper = mapper;
        _friendRepository = friendRepository;
        _pozdrikRepository = pozdrikRepository;
    }
    
    public async Task<bool> AddFriendInListAsync(FriendDto friend)
    {
        FriendList friendList = new FriendList
        {
            Appid = friend.AppId,
            FriendUsername = friend.FriendUsername,
            FriendName = friend.FriendName,
            DateBirth = friend.DateBirth,
            
        };
        return await _friendRepository.AddFriendInListAsync(friendList);
        
    }

    public async Task<bool> DeleteFriendFromListAsync(DeleteFriendDto friend)
    {
        return await _friendRepository.DeleteFriendFromListAsync(friend.AppId,friend.FriendUsername);
    }

    public async Task<bool> UpdateFriendInListAsync(FriendDto friend)
    {
        FriendList friendList = new FriendList
        {
            Appid = friend.AppId,
            FriendUsername = friend.FriendUsername,
            FriendName = friend.FriendName,
            DateBirth = friend.DateBirth,
            
        };
        return await _friendRepository.UpdateFriendInListAsync(friendList);
    }

    public async Task<List<FriendDto>> SelectByAppIdAsync(AppIdDto appid)
    {
        
        var friends = await _friendRepository.SelectByAppIdAsync(appid.AppId);
        return _mapper.Map<List<FriendDto>>(friends);
    }

    public async Task<PozdrikIdDto> GetPozdrikIdAsync(GetPozdrikQueryDto queryDto)
    {
        return new PozdrikIdDto
            { _pozdrikId = await _friendRepository.GetPozdrikIdAsync(queryDto.Username, queryDto.Appid) };
    }

    public async Task<bool> AddIntAndPozhAsync(AddIntAndPozhDto intAndPozh)
    {
      return await _pozdrikRepository.AddIntAndPozhAsync(intAndPozh.IdPozdr, intAndPozh.Interests, intAndPozh.Pozhelania);
    }

    public async Task<AddIntAndPozhDto> SelectIntAndPozhAsync(PozdrikIdDto pozdrik)
    {
        (string?, string?) pozdr = await _pozdrikRepository.SelectIntAndPozhAsync(pozdrik._pozdrikId);
        return new AddIntAndPozhDto{ Interests = pozdr.Item1, Pozhelania = pozdr.Item2};
    }
}