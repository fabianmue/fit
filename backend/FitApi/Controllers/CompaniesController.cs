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

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDto>> GetCompany(int id)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<CompanyDto>(company));
    }

    [HttpPost]
    public async Task<ActionResult<CompanyDto>> PostCompany(CompanyChangeDto companyCreateDto)
    {
        var company = _mapper.Map<Company>(companyCreateDto);
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        var companyDto = _mapper.Map<CompanyDto>(company);
        return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, companyDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompany(int id, CompanyChangeDto companyCreateDto)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company == null)
        {
            return NotFound();
        }

        _mapper.Map(companyCreateDto, company);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Companies.Any(e => e.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return Ok(_mapper.Map<CompanyDto>(company));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company == null)
        {
            return NotFound();
        }

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
