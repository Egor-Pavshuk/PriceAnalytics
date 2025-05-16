using PriceAnalytics.Core.DbModels;

namespace PriceAnalytics.Core.BllModels
{
    public class DailyResultBll
    {
        public DateOnly Date { get; set; }
        public int Hour { get; set; }
        public decimal Price { get; set; }
        public decimal OfferedVolume { get; set; }
        public decimal AcceptedVolume { get; set; }
        public string ApplicationType { get; set; } = string.Empty;
        public Comment? Comment { get; set; }
    }
}
