using System.ComponentModel.DataAnnotations;

namespace PriceAnalytics.Core.DbModels
{
    public class SaleApplication
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        public List<RowData> Rows { get; set; } = [];
    }
}
