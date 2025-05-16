using PriceAnalytics.Core.BllModels;
using PriceAnalytics.Core.DbModels;
using PriceAnalytics.Core.OperationStatuses;

namespace PriceAnalytics.Core.Interfaces
{
    public interface IPriceAnalyticsRepository
    {
        Task<OperationStatus> SaveOrderToDatabaseAsync(SaleApplication saleApplication);
        Task<OperationStatus> SavePricesToDatabaseAsync(List<DailyResult> dailyResults);
        Task<List<DailyResultBll>> GetPricesBySingleDateAsync(DateOnly date);
        Task<List<DailyResultBll>> GetPricesByDateRangeAsync(DateOnly startDate, DateOnly endDate);
        Task<List<SaleApplicationBll>> GetOrderBySingleDateAsync(DateOnly date);
        Task<List<SaleApplicationBll>> GetOrdersByDateRangeAsync(DateOnly startDate, DateOnly endDate);
        Task<bool> IsPricesDateExistAsync(DateOnly date);
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task AddAsync(DailyResult entity);
        Task UpdateAsync(SaleApplication entity);
        Task DeleteAsync(int id);
    }
}
