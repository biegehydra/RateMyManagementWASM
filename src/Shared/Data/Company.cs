using System.ComponentModel.DataAnnotations;

namespace RateMyManagementWASM.Shared.Data
{
    public class Company : IEntity
    {
        public static readonly Company Default = new Company()
        {
            Id = Guid.NewGuid().ToString(),
            Description = "DESCRIPTION",
            Industry = "INDUSTRY",
            Locations = new List<Location>(),
            LogoDeleteUrl = string.Empty,
            LogoUrl = string.Empty,
            Name = "DEFAULT",
        };
        public static readonly string DefaultUrl = "https://i.ibb.co/WkG4Jgf/test.png";
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Industry { get; set; }
        public string? LogoUrl { get; set; }
        public string? LogoDeleteUrl { get; set; }
        public List<Location> Locations { get; set; } = new();
        public float GetRating()
        {
            return Locations.Count == 0 ? 0f : Locations.Select(x => x.GetRating()).Average();
        }

        public string GetLogoUrl()
        {
            return string.IsNullOrWhiteSpace(LogoUrl) ? DefaultUrl : LogoUrl;
        }
    }
}
