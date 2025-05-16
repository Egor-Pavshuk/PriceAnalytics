using PriceAnalytics.Core.Enums;

namespace PriceAnalytics.Core.OperationStatuses
{
    public class OperationStatus
    {
        public OperationResultStatus Status { get; set; }
        public string? Message { get; set; }
    }
}
