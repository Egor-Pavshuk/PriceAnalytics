using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Core.BllModels;
using PriceAnalytics.Core.DbModels;
using PriceAnalytics.Core.Enums;
using PriceAnalytics.Core.Interfaces;
using PriceAnalytics.Core.OperationStatuses;

namespace PriceAnalytics.DAL
{
    public class PriceAnalyticsRepository : IPriceAnalyticsRepository
    {
        private readonly PriceAnalyticsDbContext _context;

        public PriceAnalyticsRepository(PriceAnalyticsDbContext context)
        {
            _context = context;
        }

        public async Task<OperationStatus> SaveOrderToDatabaseAsync(SaleApplication saleApplication)
        {
            var result = new OperationStatus()
            {
                Status = OperationResultStatus.Failed,
            };

            try
            {
                if (saleApplication != null)
                {
                    var targetDate = saleApplication.Date;

                    var existingData = await _context.SaleApplications
                        .Where(d => d.Date == targetDate)
                        .ToListAsync();

                    if (existingData.Count > 0)
                    {
                        _context.SaleApplications.RemoveRange(existingData);
                    }

                    await _context.SaleApplications.AddAsync(saleApplication);
                    await _context.SaveChangesAsync();

                    result.Status = OperationResultStatus.Success;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return result;
        }

        public async Task<OperationStatus> SavePricesToDatabaseAsync(List<DailyResult> dailyResults)
        {
            var result = new OperationStatus()
            {
                Status = OperationResultStatus.Failed,
            };

            try
            {
                if (dailyResults != null && dailyResults.Count != 0)
                {
                    var targetDate = dailyResults[0].Date;

                    var existingData = await _context.DailyResults
                        .Where(d => d.Date == targetDate)
                        .ToListAsync();

                    if (existingData.Count > 0)
                    {
                        _context.DailyResults.RemoveRange(existingData);
                    }

                    await _context.DailyResults.AddRangeAsync(dailyResults);
                    await _context.SaveChangesAsync();

                    result.Status = OperationResultStatus.Success;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        public async Task<List<DailyResultBll>> GetPricesBySingleDateAsync(DateOnly date)
        {
            List<DailyResultBll> result = [];

            try
            {
                result = await _context.DailyResults
                    .Where(d => d.Date == date)
                    .Select(d => new DailyResultBll()
                    {
                        Date = d.Date,
                        Hour = d.Hour,
                        Price = d.Price,
                        OfferedVolume = d.OfferedVolume,
                        AcceptedVolume = d.AcceptedVolume,
                        ApplicationType = d.ApplicationType
                    })
                    .OrderBy(d => d.Hour)
                    .ToListAsync();
            }
            catch (Exception)
            { }

            return result;
        }

        public async Task<List<DailyResultBll>> GetPricesByDateRangeAsync(DateOnly startDate, DateOnly endDate)
        {
            List<DailyResultBll> result = [];

            try
            {
                result = await _context.DailyResults
                    .Where(d => d.Date >= startDate && d.Date <= endDate)
                    .Select(d => new DailyResultBll()
                    {
                        Date = d.Date,
                        Hour = d.Hour,
                        Price = d.Price,
                        OfferedVolume = d.OfferedVolume,
                        AcceptedVolume = d.AcceptedVolume,
                        ApplicationType = d.ApplicationType
                    })
                    .OrderBy(d => d.Hour)
                    .OrderBy(d => d.Date)
                    .ToListAsync();

            }
            catch (Exception)
            { }

            return result;
        }

        public async Task<List<SaleApplicationBll>> GetOrderBySingleDateAsync(DateOnly date)
        {
            List<SaleApplicationBll> result = [];

            try
            {
                result.Add(await _context.SaleApplications
                    .Select(s => new SaleApplicationBll
                    {
                        Date = s.Date,
                        ApplicationType = s.ApplicationType,
                        Rows = s.Rows.Select(r => new RowDataBll
                        {
                            Hours = r.Hours.Select(h => new HourDataBll
                            {
                                Hour = h.Hour,
                                Price = h.Price,
                                Volume = h.Volume,
                            }).ToList(),
                        }).ToList()
                    })
                    .FirstAsync(s => s.Date == date));
            }
            catch (Exception)
            { }

            return result;
        }


        public async Task<List<SaleApplicationBll>> GetOrdersByDateRangeAsync(DateOnly startDate, DateOnly endDate)
        {
            List<SaleApplicationBll> result = [];

            try
            {
                result = await _context.SaleApplications
                    .Where(s => s.Date >= startDate && s.Date <= endDate)
                    .Select(s => new SaleApplicationBll
                    {
                        Date = s.Date,
                        ApplicationType = s.ApplicationType,
                        Rows = s.Rows.Select(r => new RowDataBll
                        {
                            Hours = r.Hours.Select(h => new HourDataBll
                            {
                                Hour = h.Hour,
                                Price = h.Price,
                                Volume = h.Volume,
                            }).ToList(),
                        }).ToList()
                    })
                    .OrderBy(s => s.Date)
                    .ToListAsync();

            }
            catch (Exception)
            { }

            return result;
        }

        public async Task<bool> IsPricesDateExistAsync(DateOnly date)
        {
            bool result = false;

            try
            {
                result = await _context.DailyResults.AnyAsync(d => d.Date == date);
            }
            catch (Exception)
            { }

            return result;
        }

        public Task AddAsync(DailyResult entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SaleApplication entity)
        {
            throw new NotImplementedException();
        }
    }
}
