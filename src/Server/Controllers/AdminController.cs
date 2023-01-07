using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateMyManagementWASM.Server.Data;
using RateMyManagementWASM.Shared;
using RateMyManagementWASM.Shared.Requests;

namespace RateMyManagementWASM.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private const string _role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    private ApplicationDbContext _context;
    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpPost("populatedb")]
    public async Task<ActionResult> PopulateDb([FromBody] PopulateDbRequest request)
    {
        if (!IsAdmin(User.Claims)) return Forbid();
        var companies = BogusWrapper.GenerateFakeCompanies(request.Companies);
        foreach (var company in companies)
        {
            var companyLocations = BogusWrapper.GenerateFakeLocations(company, request.LocationsPerCompany);
            foreach (var companyLocation in companyLocations)
            {
                var locationReviews =
                    BogusWrapper.GenerateFakeLocationReviews(companyLocation, request.LocationReviewsPerLocation);
                companyLocation.LocationReviews.AddRange(locationReviews);
            }
            company.Locations.AddRange(companyLocations);
        }
        await _context.Companies.AddRangeAsync(companies);
        await _context.SaveChangesAsync();
        return Ok();
    }

    public bool IsAdmin(IEnumerable<Claim> claims)
    {
        return claims.Any(x => x.Type == _role && x.Value == "Administrator");
    }
}