using System.ComponentModel.DataAnnotations;

namespace RateMyManagementWASM.Shared.Data
{
    public class LocationReview : IEntity
    {
        [Key]
        public string Id { get; set; }
        public Location Location { get; set; }
        public string LocationId { get; set; }
        public int Stars { get; set; }
        public string SenderUsername { get; set; }
        public string Content { get; set; }
        public string SentDateAndTime { get; set; } = DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString();
        public string Type { get; set; }
        public string ManagerType;
        public string ManagerAttributes { get; set; }
        public string ManagerName { get; set; }
    }
}
