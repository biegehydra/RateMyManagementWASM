namespace RateMyManagementWASM.Shared.Dtos
{
    public class LocationReviewDto
    {
        public string Id { get; set; }
        public string LocationId { get; set; }
        public string ApplicationUserId { get; set; }
        public int Stars { get; set; }
        public string SenderUsername { get; set; }
        public string Content { get; set; }
        public string SentDateAndTime { get; set; } = DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString();
        public string Type { get; set; }
        public string ManagerType { get; set; }
        public string ManagerAttributes { get; set; }
        public string ManagerName { get; set; }
    }
}
