using Microsoft.AspNetCore.Mvc;
using PriceAnalytics.Core.DbModels;
using PriceAnalytics.Core.Interfaces;
using System.Linq;

namespace PriceAnalytics.Controllers
{
    [ApiController]
    [Route("api/filters")]
    public class FiltersController : ControllerBase
    {
        private readonly IPriceAnalyticsService _service;

        public FiltersController(IPriceAnalyticsService service)
        {
            _service = service;
        }

        [HttpGet("prices/by-single-date")]
        public async Task<IActionResult> GetPricesBySingleDate([FromQuery] string date)
        {
            IActionResult result = BadRequest();

            try
            {
                var data = await _service.GetPricesBySingleDateAsync(DateOnly.FromDateTime(DateTime.Parse(date)));
                if (data != null)
                {
                    result = Ok(data);
                }
            }
            catch (Exception e)
            {
                result = BadRequest(e.Message);
            }

            return result;
        }

        [HttpGet("prices/by-date-range")]
        public async Task<IActionResult> GetPricesByDateRange([FromQuery] string startDate, [FromQuery] string endDate)
        {
            IActionResult result = BadRequest();

            try
            {
                var dateOnlyStart = DateOnly.FromDateTime(DateTime.Parse(startDate));
                var dateOnlyEnd = DateOnly.FromDateTime(DateTime.Parse(endDate));
                var data = await _service.GetPricesByDateRangeAsync(dateOnlyStart, dateOnlyEnd);
                if (data != null)
                {
                    result = Ok(data);
                }
            }
            catch (Exception e)
            {
                result = BadRequest(e.Message);
            }

            return result;
        }

        [HttpGet("orders/by-single-date")]
        public async Task<IActionResult> GetOrderBySingleDate([FromQuery] string date)
        {
            IActionResult result = BadRequest();

            try
            {
                var data = await _service.GetOrderBySingleDateAsync(DateOnly.FromDateTime(DateTime.Parse(date)));
                if (data != null)
                {
                    result = Ok(data);
                }
            }
            catch (Exception e)
            {
                result = BadRequest(e.Message);
            }

            return result;
        }

        [HttpGet("orders/by-date-range")]
        public async Task<IActionResult> GetOrdersByDateRange([FromQuery] string startDate, [FromQuery] string endDate)
        {
            IActionResult result = BadRequest();

            try
            {
                var dateOnlyStart = DateOnly.FromDateTime(DateTime.Parse(startDate));
                var dateOnlyEnd = DateOnly.FromDateTime(DateTime.Parse(endDate));
                var data = await _service.GetOrdersByDateRangeAsync(dateOnlyStart, dateOnlyEnd);
                if (data != null)
                {
                    result = Ok(data);
                }
            }
            catch (Exception e)
            {
                result = BadRequest(e.Message);
            }

            return result;
        }
    }
}
