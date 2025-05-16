using System.ComponentModel.DataAnnotations;

namespace PriceAnalytics.Core.DbModels
{
    public class DailyResult
    {
        [Key]
        public int Id { get; set; }
        public Comment? Comment { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public int Hour { get; set; }
        public decimal Price { get; set; }
        public decimal OfferedVolume { get; set; }
        public decimal AcceptedVolume { get; set; }
        public string ApplicationType { get; set; } = string.Empty;
    }
}
