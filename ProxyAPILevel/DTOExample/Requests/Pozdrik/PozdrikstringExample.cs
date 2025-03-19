using Domain.DTO.DTO.Pozdr;
using Entities.Templates;
using Swashbuckle.AspNetCore.Filters;

namespace ProxyAPILeval.DTOExample;

public class PozdrikstringExample: IExamplesProvider<PozdrStringDTO>
{
    public PozdrStringDTO GetExamples()
    {
        return new PozdrStringDTO
        {
           _pozdrikId = 2,
           _pozdr = "efefaiwjijpaifjkpiafjoawfpoawfkpaof"
        };
    }
}