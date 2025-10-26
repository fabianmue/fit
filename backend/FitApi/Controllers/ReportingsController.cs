using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT.FitApi;

[ApiController]
[Route("api/companies/{companyId}/[controller]")]
public class ReportingsController(FitApiContext context, IMapper mapper) : ControllerBase
{
    private readonly FitApiContext _context = context;

    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<ActionResult<ReportingDto>> PostReporting(
        [FromRoute] int companyId,
        [FromBody] ReportingChangeDto reportingChangeDto
    )
    {
        var company = await _context.Companies.FindAsync(companyId);
        if (company == null)
        {
            return NotFound();
        }

        var reporting = _mapper.Map<Reporting>(reportingChangeDto);
        reporting.CompanyId = companyId;

        _context.Reportings.Add(reporting);
        await _context.SaveChangesAsync();

        var reportingDto = _mapper.Map<ReportingDto>(reporting);
        return Ok(reportingDto);
    }

    [HttpPut("{reportingId}")]
    public async Task<IActionResult> PutReporting(
        [FromRoute] int companyId,
        [FromRoute] int reportingId,
        [FromBody] ReportingChangeDto reportingChangeDto
    )
    {
        var reporting = await _context.Reportings.FirstOrDefaultAsync(r =>
            r.Id == reportingId && r.CompanyId == companyId
        );
        if (reporting == null)
        {
            return NotFound();
        }

        _mapper.Map(reportingChangeDto, reporting);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Reportings.Any(r => r.Id == reportingId && r.CompanyId == companyId))
            {
                return NotFound();
            }
            throw;
        }

        return Ok(_mapper.Map<ReportingDto>(reporting));
    }

    [HttpDelete("{reportingId}")]
    public async Task<IActionResult> DeleteReporting(
        [FromRoute] int companyId,
        [FromRoute] int reportingId
    )
    {
        var reporting = await _context.Reportings.FirstOrDefaultAsync(r =>
            r.Id == reportingId && r.CompanyId == companyId
        );
        if (reporting == null)
        {
            return NotFound();
        }

        _context.Reportings.Remove(reporting);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
