using OfficeOpenXml;
using PriceAnalytics.Core.BllModels;
using PriceAnalytics.Core.DbModels;
using PriceAnalytics.Core.Enums;
using PriceAnalytics.Core.Interfaces;
using PriceAnalytics.Core.OperationStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PriceAnalytics.Bll
{
    public class PriceAnalyticsService : IPriceAnalyticsService
    {
        private readonly IPriceAnalyticsRepository _repository;
        public PriceAnalyticsService(IPriceAnalyticsRepository repository)
        {
            _repository = repository;
        }

        public Task AddAsync(Customer model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DailyResultBll>> GetPricesByDateRangeAsync(DateOnly startDate, DateOnly endDate)
            => await _repository.GetPricesByDateRangeAsync(startDate, endDate);

        public async Task<List<DailyResultBll>> GetPricesBySingleDateAsync(DateOnly date)
            => await _repository.GetPricesBySingleDateAsync(date);

        public async Task<List<SaleApplicationBll>> GetOrdersByDateRangeAsync(DateOnly startDate, DateOnly endDate)
            => await _repository.GetOrdersByDateRangeAsync(startDate, endDate);

        public async Task<List<SaleApplicationBll>> GetOrderBySingleDateAsync(DateOnly date)
            => await _repository.GetOrderBySingleDateAsync(date);

        public async Task<OperationStatus> SaveOrderDataAsync(Stream fileStream, string fileName)
        {
            var resultStatus = new OperationStatus()
            {
                Status = OperationResultStatus.Failed,
            };
            SaleApplication saleApplication = new();
            List<RowData> rowsData = [];
            RowData firstRowData = new()
            {
                Hours = []
            };
            new EPPlusLicense().SetNonCommercialPersonal("Egp");

            try
            {
                using (ExcelPackage package = new())
                {
                    await package.LoadAsync(fileStream);
                    var worcksheet = package.Workbook.Worksheets[0];
                    var dateCell = worcksheet.Cells[1, 6];
                    if (dateCell.Value is string value && dateCell.Style.Numberformat.Format.Contains("yy"))
                    {
                        saleApplication.Date = DateOnly.FromDateTime(DateTime.Parse(value));
                    }

                    for (int i = 3; i <= 27; i++)
                    {
                        var volumeCell = worcksheet.Cells[8, i];
                        var priceCell = worcksheet.Cells[9, i];

                        if (!string.IsNullOrEmpty(volumeCell.Text) && !string.IsNullOrEmpty(priceCell.Text))
                        {
                            HourData hourData = new()
                            {
                                Hour = i - 2,
                                Volume = decimal.Parse(volumeCell.Text),
                                Price = decimal.Parse(priceCell.Text),
                            };

                            firstRowData.Hours.Add(hourData);
                        }

                        
                    }

                    rowsData.Add(firstRowData);

                    for (int i = 12; i <= 42; i += 3)
                    {
                        RowData rowData = new()
                        {
                            Hours = []
                        };

                        for (int j = 3; j <= 27; j++)
                        {
                            var volumeCell = worcksheet.Cells[i, j];
                            var priceCell = worcksheet.Cells[i + 1, j];

                            if (!string.IsNullOrEmpty(volumeCell.Text) && !string.IsNullOrEmpty(priceCell.Text))
                            {
                                HourData hourData = new()
                                {
                                    Hour = j - 2,
                                    Volume = decimal.Parse(volumeCell.Text),
                                    Price = decimal.Parse(priceCell.Text),
                                };

                                rowData.Hours.Add(hourData);
                            }
                        }

                        if (rowData.Hours.Count != 0)
                        {
                            rowsData.Add(rowData);
                        }
                    }

                    saleApplication.Rows = rowsData;
                }

                resultStatus = await _repository.SaveOrderToDatabaseAsync(saleApplication);
            }
            catch (Exception e)
            {
                resultStatus.Message = e.Message;
            }

            return resultStatus;
        }

        public async Task<OperationStatus> SavePricesDataAsync(Stream fileStream, string fileName)
        {
            var resultStatus = new OperationStatus()
            {
                Status = OperationResultStatus.Failed,
            };
            List<DailyResult> result = [];
            DateOnly date = new();
            new EPPlusLicense().SetNonCommercialPersonal("Egp");

            try
            {
                using (ExcelPackage package = new())
                {
                    await package.LoadAsync(fileStream);
                    var worcksheet = package.Workbook.Worksheets[1];

                    var dateCell = worcksheet.Cells[2, 1];
                    if (dateCell.Value is double value && dateCell.Style.Numberformat.Format.Contains("yy"))
                    {
                        date = DateOnly.FromDateTime(DateTime.FromOADate((double)value));
                    }

                    for (int i = 2; i <= worcksheet.DimensionByValue.Rows; i++)
                    {
                        var dailyResult = new DailyResult
                        {
                            Date = date,
                            Hour = int.Parse(worcksheet.Cells[i, 2].Text),
                            Price = decimal.Parse(worcksheet.Cells[i, 3].Text),
                            OfferedVolume = decimal.Parse(worcksheet.Cells[i, 4].Text),
                            AcceptedVolume = decimal.Parse(worcksheet.Cells[i, 5].Text),
                            ApplicationType = worcksheet.Cells[i, 6].Text
                        };

                        result.Add(dailyResult);
                    }
                }

                resultStatus = await _repository.SavePricesToDatabaseAsync(result);
            }
            catch (Exception e)
            {
                resultStatus.Message = e.Message;
            }

            return resultStatus;
        }

        public Task UpdateAsync(Customer model)
        {
            throw new NotImplementedException();
        }
    }
}
