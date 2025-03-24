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
    //nado
    public async Task<bool> AddPozdrikIdToFriendAsync(AddPozdrIdDto pozdrId)
    {
        return await _friendRepository.AddPozdrikIdToFriendAsync(pozdrId.PozdrikId, pozdrId.FriendUsername, pozdrId.AppId);
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
    //nado
    public async Task<PozdrikIdDto> CreatePozdrikAsync(string? interests, string? pozhelania)
    {
        return new PozdrikIdDto { _pozdrikId = await _pozdrikRepository.CreatePozdrikAsync(interests, pozhelania) };
    }

    public async Task<bool> AddFriendAndWishAsync(FriendDto friendDto, AddIntAndPozhDto pozhDto)
    {
        FriendList friendList = new FriendList
        {
            Appid = friendDto.AppId,
            FriendUsername = friendDto.FriendUsername,
            FriendName = friendDto.FriendName,
            DateBirth = friendDto.DateBirth,
        };
        bool res1=await _friendRepository.AddFriendInListAsync(friendList);
        int res2= await _pozdrikRepository.CreatePozdrikAsync(pozhDto.Interests, pozhDto.Pozhelania);
        bool res3 = await _friendRepository.AddPozdrikIdToFriendAsync(res2,friendDto.FriendUsername,friendDto.AppId);
        if (res1 && res3) return true;
        return false;
        
    }
}