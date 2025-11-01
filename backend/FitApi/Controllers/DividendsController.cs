using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT.FitApi;

[ApiController]
[Route("api/companies/{companyId}/[controller]")]
public class DividendsController(FitApiContext context, IMapper mapper) : ControllerBase
{
    private readonly FitApiContext _context = context;

    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<ActionResult<DividendDto>> PostDividend(
        [FromRoute] int companyId,
        [FromBody] DividendChangeDto dividendChangeDto
    )
    {
        var company = await _context.Companies.FindAsync(companyId);
        if (company == null)
        {
            return NotFound();
        }

        var dividend = _mapper.Map<Dividend>(dividendChangeDto);
        dividend.CompanyId = companyId;

        _context.Dividends.Add(dividend);
        await _context.SaveChangesAsync();

        var dividendDto = _mapper.Map<DividendDto>(dividend);
        return Ok(dividendDto);
    }

    [HttpPut("{dividendId}")]
    public async Task<IActionResult> PutDividend(
        [FromRoute] int companyId,
        [FromRoute] int dividendId,
        [FromBody] DividendChangeDto dividendChangeDto
    )
    {
        var dividend = await _context.Dividends.FirstOrDefaultAsync(d =>
            d.Id == dividendId && d.CompanyId == companyId
        );
        if (dividend == null)
        {
            return NotFound();
        }

        _mapper.Map(dividendChangeDto, dividend);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Dividends.Any(d => d.Id == dividendId && d.CompanyId == companyId))
            {
                return NotFound();
            }
            throw;
        }

        return Ok(_mapper.Map<DividendDto>(dividend));
    }

    [HttpDelete("{dividendId}")]
    public async Task<IActionResult> DeleteDividend(
        [FromRoute] int companyId,
        [FromRoute] int dividendId
    )
    {
        var dividend = await _context.Dividends.FirstOrDefaultAsync(d =>
            d.Id == dividendId && d.CompanyId == companyId
        );
        if (dividend == null)
        {
            return NotFound();
        }

        _context.Dividends.Remove(dividend);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
