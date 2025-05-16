using System.ComponentModel.DataAnnotations;

namespace PriceAnalytics.Core.DbModels
{
    public class Comment
    {
        [Required]
        public uint Id { get; set; }
        [Required]
        public Customer Customer { get; set; } = new();
        [Required]
        public string Description { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = string.Empty;
    }
}
