using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT.FitApi;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController(FitApiContext context, IMapper mapper) : ControllerBase
{
    private readonly FitApiContext _context = context;

    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
    {
        var companies = await _context.Companies.ToListAsync();
        return _mapper.Map<List<CompanyDto>>(companies);
    }

    [HttpGet("{companyId:int}")]
    public async Task<ActionResult<CompanyDto>> GetCompany([FromRoute] int companyId)
    {
        var company = await _context
            .Companies.Include(c => c.Reportings)
            .FirstOrDefaultAsync(c => c.Id == companyId);
        if (company == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<CompanyDto>(company));
    }

    [HttpPost]
    public async Task<ActionResult<CompanyDto>> PostCompany(
        [FromBody] CompanyChangeDto companyChangeDto
    )
    {
        var company = _mapper.Map<Company>(companyChangeDto);
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        var companyDto = _mapper.Map<CompanyDto>(company);
        return CreatedAtAction(nameof(GetCompany), new { companyId = company.Id }, companyDto);
    }

    [HttpPut("{companyId:int}")]
    public async Task<IActionResult> PutCompany(
        [FromRoute] int companyId,
        [FromBody] CompanyChangeDto companyChangeDto
    )
    {
        var company = await _context.Companies.FindAsync(companyId);
        if (company == null)
        {
            return NotFound();
        }

        _mapper.Map(companyChangeDto, company);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Companies.Any(c => c.Id == companyId))
            {
                return NotFound();
            }
            throw;
        }

        return Ok(_mapper.Map<CompanyDto>(company));
    }

    [HttpDelete("{companyId:int}")]
    public async Task<IActionResult> DeleteCompany([FromRoute] int companyId)
    {
        var company = await _context.Companies.FindAsync(companyId);
        if (company == null)
        {
            return NotFound();
        }

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
