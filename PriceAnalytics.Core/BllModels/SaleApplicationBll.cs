using System.ComponentModel.DataAnnotations;

namespace PriceAnalytics.Core.BllModels
{
    public class SaleApplicationBll
    {
        public DateOnly Date { get; set; }
        public List<RowDataBll> Rows { get; set; } = [];
    }
}
