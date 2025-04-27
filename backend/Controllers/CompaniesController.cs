using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class CompaniesController(FitBackendContext context, IMapper mapper) : ControllerBase
{
  private readonly FitBackendContext _context = context;

  private readonly IMapper _mapper = mapper;

  [HttpGet]
  [Produces("application/json")]
  [ProducesResponseType(typeof(List<CompanyReadDto>), StatusCodes.Status200OK)]
  public async Task<IActionResult> GetCompanies()
  {
    var companies = await AddDefaultIncludes(_context.Set<Company>()).AsNoTracking().ToListAsync();
    return Ok(_mapper.Map<List<CompanyReadDto>>(companies));
  }

  [HttpGet("{id}")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(CompanyReadDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetCompany([FromRoute] Guid id)
  {
    var company = await AddDefaultIncludes(_context.Set<Company>())
      .AsNoTracking()
      .FirstOrDefaultAsync(company => company.Id == id);
    if (company == null)
    {
      return NotFound();
    }

    return Ok(_mapper.Map<CompanyReadDto>(company));
  }

  [HttpPost]
  [Authorize("Authenticated")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(CompanyReadDto), StatusCodes.Status201Created)]
  public async Task<IActionResult> PostCompany([FromBody] CompanyCreateDto companyCreateDto)
  {
    var company = _mapper.Map<Company>(companyCreateDto);
    _context.Set<Company>().Add(company);
    await _context.SaveChangesAsync();

    var companyReadDto = _mapper.Map<CompanyReadDto>(company);
    return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, companyReadDto);
  }

  [HttpPut("{id}")]
  [Authorize("Authenticated")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(CompanyReadDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> PutCompany(
    [FromRoute] Guid id,
    [FromBody] CompanyUpdateDto companyUpdateDto
  )
  {
    var company = await TryGetCompanyAsync(id);
    if (company == null)
    {
      return NotFound();
    }

    company.Links.Clear();
    _mapper.Map(companyUpdateDto, company);
    _context.Entry(company).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!CompanyExists(id))
      {
        return NotFound();
      }
      else
      {
        throw;
      }
    }

    var companyReadDto = _mapper.Map<CompanyReadDto>(company);
    return Ok(companyReadDto);
  }

  [HttpDelete("{id}")]
  [Authorize("Authenticated")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteCompany([FromRoute] Guid id)
  {
    var company = await TryGetCompanyAsync(id);
    if (company == null)
    {
      return NotFound();
    }

    _context.Set<Company>().Remove(company);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  private bool CompanyExists(Guid id)
  {
    return _context.Set<Company>().Any(company => company.Id == id);
  }

  private async Task<Company?> TryGetCompanyAsync(Guid id)
  {
    return await _context
      .Set<Company>()
      .Include(company => company.Links)
      .FirstOrDefaultAsync(company => company.Id == id);
  }

  protected IQueryable<Company> AddDefaultIncludes(IQueryable<Company> companies)
  {
    return companies
      .Include(company => company.Links)
      .Include(company => company.CompanyTextCharacteristics)
      .ThenInclude(companyCharacteristic => companyCharacteristic.TextCharacteristic)
      .Include(company => company.CompanyNumberCharacteristics)
      .ThenInclude(companyCharacteristic => companyCharacteristic.NumberCharacteristic)
      .Include(company => company.CompanyHistoricNumberCharacteristics)
      .ThenInclude(companyCharacteristic => companyCharacteristic.HistoricNumberCharacteristic)
      .Include(company => company.CompanyHistoricNumberCharacteristics)
      .ThenInclude(companyCharacteristic => companyCharacteristic.Values)
      .Include(company => company.CompanyHistoricFinancialCharacteristics)
      .ThenInclude(companyCharacteristic => companyCharacteristic.HistoricFinancialCharacteristic)
      .Include(company => company.CompanyHistoricFinancialCharacteristics)
      .ThenInclude(companyCharacteristic => companyCharacteristic.Values);
  }
}
