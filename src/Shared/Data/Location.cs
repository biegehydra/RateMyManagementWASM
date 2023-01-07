using System.ComponentModel.DataAnnotations;

namespace RateMyManagementWASM.Shared.Data
{
    public class Location : IEntity
    {
        public static readonly Location Default = new Location()
        {
            Id = Guid.NewGuid().ToString(),
            City = "Default City",
            Address = "Default Address",
            LocationReviews = new List<LocationReview>(),
        };
        [Key]
        public string Id { get; set; }
        public Company Company { get; set; }
        public string CompanyId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public List<LocationReview> LocationReviews { get; set; } = new();
        public float GetRating()
        {
            return LocationReviews.Count == 0 ? 0f : (float) LocationReviews.Average(x => x.Stars);
        }
    }
}
