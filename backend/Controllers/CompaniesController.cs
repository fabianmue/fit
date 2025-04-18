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
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<List<CompanyReadDto>>> GetCompanies()
  {
    var companies = await _context
      .Companies.Include(company => company.CompanyCharacteristics)
      .ThenInclude(companyCharacteristic => companyCharacteristic.Characteristic)
      .Include(company => company.CompanyHistoricCharacteristics)
      .ThenInclude(companyHistoricCharacteristic =>
        companyHistoricCharacteristic.HistoricCharacteristic
      )
      .Include(company => company.CompanyHistoricCharacteristics)
      .ThenInclude(companyHistoricCharacteristic => companyHistoricCharacteristic.Values)
      .AsNoTracking()
      .ToListAsync();
    return Ok(_mapper.Map<List<CompanyReadDto>>(companies));
  }

  [HttpGet("{id}")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<CompanyReadDto>> GetCompany([FromRoute] Guid id)
  {
    var company = await _context
      .Companies.Include(company => company.CompanyCharacteristics)
      .ThenInclude(companyCharacteristic => companyCharacteristic.Characteristic)
      .Include(company => company.CompanyHistoricCharacteristics)
      .ThenInclude(companyHistoricCharacteristic =>
        companyHistoricCharacteristic.HistoricCharacteristic
      )
      .Include(company => company.CompanyHistoricCharacteristics)
      .ThenInclude(companyHistoricCharacteristic => companyHistoricCharacteristic.Values)
      .AsNoTracking()
      .FirstOrDefaultAsync(company => company.Id == id);
    if (company == null)
    {
      return NotFound();
    }

    return Ok(_mapper.Map<CompanyReadDto>(company));
    ;
  }

  [HttpPost]
  [Authorize("Authenticated")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public async Task<ActionResult<CompanyReadDto>> PostCompany(
    [FromBody] CompanyCreateDto companyCreateDto
  )
  {
    var company = _mapper.Map<Company>(companyCreateDto);
    _context.Companies.Add(company);
    await _context.SaveChangesAsync();

    var companyReadDto = _mapper.Map<CompanyReadDto>(company);
    return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, companyReadDto);
  }

  [HttpPut("{id}")]
  [Authorize("Authenticated")]
  [Consumes("application/json")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
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

    return NoContent();
  }

  [HttpDelete("{id}")]
  [Authorize("Authenticated")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteCompany([FromRoute] Guid id)
  {
    var company = await TryGetCompanyAsync(id);
    if (company == null)
    {
      return NotFound();
    }

    _context.Companies.Remove(company);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  private bool CompanyExists(Guid id)
  {
    return _context.Companies.Any(company => company.Id == id);
  }

  private async Task<Company?> TryGetCompanyAsync(Guid id)
  {
    return await _context.Companies.FindAsync(id);
  }
}
