using Domain.DTO.DTO.Pozdr;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample;

public class GetCongrStringExample : IExamplesProvider<PozdrikIdDto>
{
    public PozdrikIdDto GetExamples()
    {
        return new PozdrikIdDto
        {
            _pozdrikId = 6
        };
    }}