using PriceAnalytics.Core.BllModels;
using PriceAnalytics.Core.DbModels;
using PriceAnalytics.Core.OperationStatuses;

namespace PriceAnalytics.Core.Interfaces
{
    public interface IPriceAnalyticsService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task AddAsync(Customer model);
        Task<List<DailyResultBll>> GetPricesBySingleDateAsync(DateOnly date);
        Task<List<DailyResultBll>> GetPricesByDateRangeAsync(DateOnly startDate, DateOnly endDate);
        Task<List<SaleApplicationBll>> GetOrderBySingleDateAsync(DateOnly date);
        Task<List<SaleApplicationBll>> GetOrdersByDateRangeAsync(DateOnly startDate, DateOnly endDate);
        Task<OperationStatus> SaveOrderDataAsync(Stream fileStream, string fileName);
        Task<OperationStatus> SavePricesDataAsync(Stream fileStream, string fileName);
        Task UpdateAsync(Customer model);
        Task DeleteAsync(int id);
    }
}
