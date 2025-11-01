using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT.FitApi;

[ApiController]
[Route("api/companies/{companyId}/[controller]")]
public class SharePricesController(FitApiContext context, IMapper mapper) : ControllerBase
{
    private readonly FitApiContext _context = context;

    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<ActionResult<SharePriceDto>> PostSharePrice(
        [FromRoute] int companyId,
        [FromBody] SharePriceChangeDto sharePriceChangeDto
    )
    {
        var company = await _context.Companies.FindAsync(companyId);
        if (company == null)
        {
            return NotFound();
        }

        var sharePrice = _mapper.Map<SharePrice>(sharePriceChangeDto);
        sharePrice.CompanyId = companyId;

        _context.SharePrices.Add(sharePrice);
        await _context.SaveChangesAsync();

        var sharePriceDto = _mapper.Map<SharePriceDto>(sharePrice);
        return Ok(sharePriceDto);
    }

    [HttpPut("{sharePriceId}")]
    public async Task<IActionResult> PutSharePrice(
        [FromRoute] int companyId,
        [FromRoute] int sharePriceId,
        [FromBody] SharePriceChangeDto sharePriceChangeDto
    )
    {
        var sharePrice = await _context.SharePrices.FirstOrDefaultAsync(s =>
            s.Id == sharePriceId && s.CompanyId == companyId
        );
        if (sharePrice == null)
        {
            return NotFound();
        }

        _mapper.Map(sharePriceChangeDto, sharePrice);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.SharePrices.Any(s => s.Id == sharePriceId && s.CompanyId == companyId))
            {
                return NotFound();
            }
            throw;
        }

        return Ok(_mapper.Map<SharePriceDto>(sharePrice));
    }

    [HttpDelete("{sharePriceId}")]
    public async Task<IActionResult> DeleteSharePrice(
        [FromRoute] int companyId,
        [FromRoute] int sharePriceId
    )
    {
        var sharePrice = await _context.SharePrices.FirstOrDefaultAsync(s =>
            s.Id == sharePriceId && s.CompanyId == companyId
        );
        if (sharePrice == null)
        {
            return NotFound();
        }

        _context.SharePrices.Remove(sharePrice);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
