using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

namespace UseCases.Services;

public class NeiroGenService:INeiroGenService
{
    private readonly IPozdrikRepository _pozdrikRepository;
    private readonly IFriendRepository _friendRepository;
    

    public NeiroGenService(IPozdrikRepository pozdrikRepository, IFriendRepository friendRepository)
    {
        _pozdrikRepository = pozdrikRepository;
        _friendRepository = friendRepository;
    }
    
    public async Task<AddIntAndPozhDto> SelectIntAndPozhAsync(PozdrikIdDto pozdrikId)
    {
        (string?, string?) pozdr = await _pozdrikRepository.SelectIntAndPozhAsync(pozdrikId._pozdrikId);
        return new AddIntAndPozhDto{Interests = pozdr.Item1, Pozhelania = pozdr.Item2};
    }

    public async Task<bool> AddPozdrAsync(PozdrStringDTO pozdr)
    {
        return await _pozdrikRepository.AddPozdrAsync(pozdr._pozdrikId, pozdr._pozdr);
    }

    public async Task<string?> GetNameByPozdrikId(PozdrikIdDto pozdrikId)
    {
        return await _friendRepository.GetFrienNameByPozdrikId(pozdrikId._pozdrikId);
    }

    public async Task<string?> GetUserNameByPozdrikId(PozdrikIdDto pozdrikId)
    {
        return await _friendRepository.GetFrienUserNameByPozdrikId(pozdrikId._pozdrikId);
        
    }

    public async Task<List<int>> SelectEmptyOrNullPozdrstringAsync()
    {
        return await _pozdrikRepository.SelectEmptyOrNullPozdrstringAsync();
    }
}