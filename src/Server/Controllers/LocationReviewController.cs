using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyManagementWASM.Server.Data;
using RateMyManagementWASM.Shared.Data;
using RateMyManagementWASM.Shared.Dtos;

namespace RateMyManagementWASM.Server.Controllers
{
    [ApiController]
    [Route($"api/[controller]")]
    public class LocationReviewController : ControllerBase, IEntityController<LocationReview, LocationReviewDto>
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        public LocationReviewController(ApplicationDbContext context, IMapper _mapper)
        {
            _context = context;
        }
        [HttpPost("{id}")]
        public async Task<ActionResult<LocationReview>> Get(string id, [FromBody] string[] includes)
        {
            var result = await _context.Set<LocationReview>()
                .IncludeMultiple(includes)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (result == null)
            {
                return BadRequest(nameof(LocationReview) + "Id does not exist");
            }

            return Ok(result);
        }


        [HttpPut]
        public async Task<ActionResult> Save([FromBody] LocationReviewDto entity)
        {
            var location = await _context.Locations.Include(x => x.LocationReviews).FirstOrDefaultAsync(x => x.Id == entity.LocationId);
            var user = await _context.Users.FindAsync(entity.ApplicationUserId);
            if (location == null)
            {
                return NotFound();
            }

            var locationReview = new LocationReview()
            {
                Id = entity.Id,
                ApplicationUserId = entity.ApplicationUserId,
                Content = entity.Content,
                Location = location,
                LocationId = entity.LocationId,
                ManagerAttributes = entity.ManagerAttributes,
                ManagerName = entity.ManagerName,
                ManagerType = entity.ManagerType,
                SenderUsername = entity.SenderUsername,
                SentDateAndTime = entity.SentDateAndTime,
                Stars = entity.Stars,
                Type = entity.Type
            };
            // If the location already has this locationReview
            if (location.LocationReviews != null && location.LocationReviews.Any(x => x.Id == locationReview.Id))
            {
                // find the index of it and replace it
                var index = location.LocationReviews.FindIndex(x => x.Id == locationReview.Id);
                location.LocationReviews[index] = locationReview;
            }
            else
            {
                location.LocationReviews?.Add(locationReview);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Route("createmany")]
        [HttpPost]
        public async Task<ActionResult> CreateMany([FromBody] IEnumerable<LocationReviewDto> entities)
        {
            var entityIds = entities.Select(x => x.LocationId);
            var locations = _context.Locations.Include(x => x.LocationReviews).Where(x => entityIds.Contains(x.Id));
            foreach (var entity in entities)
            {
                var location = await locations.FirstAsync(x => x.Id == entity.LocationId);
                var user = await _context.Users.FindAsync(entity.ApplicationUserId);
                if (user == null) return NotFound();
                var locationReview = new LocationReview()
                {
                    Id = entity.Id,
                    ApplicationUserId = entity.ApplicationUserId,
                    Content = entity.Content,
                    Location = location,
                    LocationId = entity.LocationId,
                    ManagerAttributes = entity.ManagerAttributes,
                    ManagerName = entity.ManagerName,
                    ManagerType = entity.ManagerType,
                    SenderUsername = entity.SenderUsername,
                    SentDateAndTime = entity.SentDateAndTime,
                    Stars = entity.Stars,
                    Type = entity.Type
                };
                location.LocationReviews?.Add(locationReview);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Route("getall")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<LocationReview>>> GetAll([FromBody] string[] includes)
        {
            try
            {
                var result = _context.LocationReviews
                    .IncludeMultiple(includes)
                    .AsNoTracking()
                    .AsEnumerable();
                Response.Headers.Append("Content-Encoding", "br");
                return Ok(result);
            }
            catch (Exception ex)
            {
                var e = ex;
            }

            return BadRequest();
        }
        [Route("test")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationReview>>> Test()
        {
            var test = Request.Headers.ToList();
            try
            {
                var result = _context.LocationReviews
                    .AsNoTracking()
                    .AsEnumerable();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var e = ex;
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] LocationReviewDto entity)
        {
            var review = await _context.Set<LocationReview>().FindAsync(entity.Id);
            if (review == null)
            {
                return NotFound();
            }
            _context.LocationReviews.Remove(review);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
