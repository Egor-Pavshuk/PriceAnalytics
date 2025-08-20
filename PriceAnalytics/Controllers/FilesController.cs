using Microsoft.AspNetCore.Mvc;
using PriceAnalytics.Core.Enums;
using PriceAnalytics.Core.Interfaces;

namespace PriceAnalytics.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IPriceAnalyticsService _service;

        public FilesController(IPriceAnalyticsService service)
        {
            _service = service;
        }

        [HttpPost("upload/order")]
        public async Task<IActionResult> UploadOrderFile(IFormFile file)
        {
            IActionResult result = BadRequest("There is no file or file is incorrect!");
#if TEST
            Thread.Sleep(4000);
            return Ok("Order");
#else
            if (file != null || file?.Length == 0)
            {
                try
                {
                    var resultStatus = await _service.SaveOrderDataAsync(file.OpenReadStream(), file.FileName);
                    if (resultStatus.Status == OperationResultStatus.Success)
                    {
                        result = Ok();
                    }
                    else
                    {
                        result = BadRequest($"{resultStatus.Message}");
                    }
                }
                catch (Exception e)
                {
                    result = BadRequest(e.Message);
                }
            }

            return result;
#endif
        }

        [HttpPost("upload/prices")]
        public async Task<IActionResult> UploadPricesFile(IFormFile file)
        {
            IActionResult result = BadRequest("There is no file or file is incorrect!");
#if TEST
            Thread.Sleep(4000);
            return Ok("Prices");
#else
            if (file != null || file?.Length == 0)
            {
                try
                {
                    var resultStatus = await _service.SavePricesDataAsync(file.OpenReadStream(), file.FileName);
                    if (resultStatus.Status == OperationResultStatus.Success)
                    {
                        result = Ok();
                    }
                    else
                    {
                        result = BadRequest($"{resultStatus.Message}");
                    }
                }
                catch (Exception e)
                {
                    result = BadRequest(e.Message);
                }
            }

            return result;
#endif
        }
    }
}
