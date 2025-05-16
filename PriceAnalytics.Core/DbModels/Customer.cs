using System.ComponentModel.DataAnnotations;

namespace PriceAnalytics.Core.DbModels
{
    public class Customer
    {
        [Required]
        public uint Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
