using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriceAnalytics.Core.DbModels
{
    public class RowData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SaleApplicationId { get; set; }

        [ForeignKey(nameof(SaleApplicationId))]
        public SaleApplication SaleApplication { get; set; } = null!;

        public List<HourData> Hours { get; set; } = new();
    }
}
