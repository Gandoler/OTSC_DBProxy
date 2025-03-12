using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Domain.Models;
using Entities.Templates;

namespace UseCases.Services;

public class NeiroGenService:INeiroGenService
{
    private readonly IPozdrikRepository _pozdrikRepository;

    public NeiroGenService(IPozdrikRepository pozdrikRepository)
    {
        _pozdrikRepository = pozdrikRepository;
    }
    
    public async Task<Pozdrik> SelectIntAndPozhAsync(PozdrikIdDto pozdrikId)
    {
        (string?, string?) pozdr = await _pozdrikRepository.SelectIntAndPozhAsync(pozdrikId._pozdrikId);
        return new Pozdrik{Interest = pozdr.Item1, Pozhelanie = pozdr.Item2};
    }

    public async Task<bool> AddPozdrAsync(PozdrStringDTO pozdr)
    {
        return await _pozdrikRepository.AddPozdrAsync(pozdr._pozdrikId, pozdr._pozdr);
    }
}