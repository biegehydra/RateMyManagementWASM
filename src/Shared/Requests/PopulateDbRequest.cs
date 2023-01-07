namespace RateMyManagementWASM.Shared.Requests;

public class PopulateDbRequest
{
    public int Companies { get; set; }
    public int LocationsPerCompany { get; set; }
    public int LocationReviewsPerLocation { get; set; }
}