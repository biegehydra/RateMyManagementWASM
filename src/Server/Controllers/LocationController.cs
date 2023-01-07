using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyManagementWASM.Server.Data;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;


namespace RateMyManagementWASM.Server.Controllers
{
    [ApiController]
    [Route($"api/[controller]")]
    public class LocationController : ControllerBase, IEntityController<Location, LocationDto>
    {
        private ApplicationDbContext _context;

        public LocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Location>> Get(string id, [FromBody] string[] includes)
        {
            var result = await _context.Set<Location>()
                .IncludeMultiple(includes)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (result == null)
            {
                return BadRequest(nameof(Location) + "Id does not exist");
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Save([FromBody] LocationDto entity)
        {
            var company = await _context.Companies.Where(x => x.Id == entity.CompanyId)
                .FirstOrDefaultAsync();
            var location = new Location()
            {
                Address = entity.Address,
                City = entity.City,
                CompanyId = entity.CompanyId,
                Id = entity.Id,
                Company = company,
                LocationReviews = new List<LocationReview>()
            };
            if (company != null)
            {
                _context.Locations.Update(location);
            }
            else
            {
                await _context.Locations.AddAsync(location);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [Route("createmany")]
        [HttpPost]
        public async Task<ActionResult> CreateMany([FromBody] IEnumerable<LocationDto> entities)
        {
            var companyIds = entities.Select(x => x.CompanyId);
            var companies = _context.Companies.Where(x => companyIds.Contains(x.Id));
            List<Location> locations = new List<Location>();
            foreach (var entity in entities)
            {
                var company = await companies.FirstOrDefaultAsync(x => x.Id == entity.CompanyId);
                var location = new Location()
                {
                    Address = entity.Address,
                    City = entity.City,
                    CompanyId = entity.CompanyId,
                    Id = entity.Id,
                    Company = company,
                    LocationReviews = new List<LocationReview>()
                };

            }
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Route("getall")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Location>>> GetAll([FromBody] string[] includes)
        {
            var result = _context.Set<Location>()
                .IncludeMultiple(includes)
                .AsNoTracking()
                .AsEnumerable();
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] LocationDto entity)
        {
            var location = _context.Locations.FirstOrDefault(x => x.Id == entity.Id);
            if (location == null)
            {
                return NotFound();
            }
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
