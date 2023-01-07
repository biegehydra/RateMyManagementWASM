namespace RateMyManagementWASM.Shared.Dtos
{
    public class CompanyDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Industry { get; set; }
        public string? LogoUrl { get; set; }
        public string? LogoDeleteUrl { get; set; }
    }
}
