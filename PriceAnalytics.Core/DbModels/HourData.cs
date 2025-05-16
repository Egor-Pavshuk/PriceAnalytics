using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriceAnalytics.Core.DbModels
{
    public class HourData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RowDataId { get; set; }

        [ForeignKey(nameof(RowDataId))]
        public RowData Row { get; set; } = null!;

        [Required]
        public int Hour { get; set; }

        [Required]
        public decimal Volume { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
