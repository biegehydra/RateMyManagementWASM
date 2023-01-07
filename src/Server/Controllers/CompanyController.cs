using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyManagementWASM.Server.Data;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;

namespace RateMyManagementWASM.Server.Controllers
{
    [ApiController]
    [Route($"api/[controller]")]
    public class CompanyController : ControllerBase, IEntityController<Company, CompanyDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CompanyController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("query/{query}")]
        public async Task<ActionResult<IEnumerable<Company>>> Query(string query, [FromBody] string[] includes,
            [FromQuery(Name = "startswith")] string startsWith)
        {
            IEnumerable<Company> response;
            if (startsWith == "true")
            {
                response = _context.Companies.IncludeMultiple(includes).Where(x => x.Name.StartsWith(query))
                    .AsEnumerable();
            }
            else
            {
                response = _context.Companies.IncludeMultiple(includes).Where(x => x.Name.Contains(query))
                    .AsEnumerable();
            }

            return Ok(response);
        }
        [HttpPost("querywithrating/{query}")]
        public async Task<ActionResult<IEnumerable<CompanyWithRatingDto>>> QueryWithRating(string query, [FromBody] string[] includes,
    [FromQuery(Name = "startswith")] string startsWith)
        {
            IEnumerable<CompanyWithRatingDto> response;
            if (startsWith == "true")
            {
                var result = _context.Companies.IncludeMultiple(includes).Where(x => x.Name.StartsWith(query))
                    .AsEnumerable();
                response = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyWithRatingDto>>(result);
            }
            else
            {
                var result = _context.Companies.IncludeMultiple(includes).Where(x => x.Name.Contains(query))
                    .AsEnumerable();
                response = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyWithRatingDto>>(result);
            }

            return Ok(response);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Company>> Get(string id, [FromBody] string[] includes)
        {
            var result = await _context.Set<Company>()
                .IncludeMultiple(includes)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (result == null)
            {
                return BadRequest(nameof(Company) + "Id does not exist");
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Save([FromBody] CompanyDto dto)
        {
            var result = await _context.Companies.Where(x => x.Id == dto.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            var company = _mapper.Map(dto, new Company());
            if (result != null)
            {
                _context.Companies.Update(company);
            }
            else
            {
                await _context.Companies.AddAsync(company);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [Route("createmany")]
        [HttpPost]
        public async Task<ActionResult> CreateMany([FromBody] IEnumerable<CompanyDto> entities)
        {
            List<Company> companies = new();
            foreach (var entity in entities)
            {
                var company = _mapper.Map(entity, new Company());
                companies.Add(company);
            }
            await _context.AddRangeAsync(companies);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Route("getall")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Company>>> GetAll([FromBody] string[] includes)
        {
            var result = _context.Set<Company>()
                .IncludeMultiple(includes)
                .AsNoTracking()
                .AsEnumerable();
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] CompanyDto entity)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (company == null)
            {
                return NotFound();
            }
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}