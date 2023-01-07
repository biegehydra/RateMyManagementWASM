namespace RateMyManagementWASM.Shared.Dtos
{
    public class CompanyWithRatingDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Industry { get; set; }
        public string LogoUrl { get; set; }
        public string? LogoDeleteUrl { get; set; }
        public float Rating { get; set; }
    }
}
