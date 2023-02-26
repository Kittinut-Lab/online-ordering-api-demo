using System;
using online_ordering_api.IService;
using online_ordering_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace online_ordering_api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ProductServiceController : ControllerBase
    {
        private ILogger<ProductServiceController> _logger;
        private IProductService _service;

        public ProductServiceController(ILogger<ProductServiceController> logger, IProductService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("products")]
        public ActionResult<ListProductsResponseEntity> ListAppProductService()
        {
            _logger.LogInformation($"ProductServiceController ListAppProductService called...");
            ListProductsResponseEntity result = null;

            try
            {
                result = _service.ListProductService();
            }
            catch (Exception ex)
            {
                string logMsg = $"ProductServiceController ListAppProductService Error : {ex.Message}";
                Console.WriteLine(logMsg);
                _logger.LogError(logMsg);
            }
            _logger.LogInformation($"ProductServiceController ListAppProductService response obj : {JsonConvert.SerializeObject(result)}");
            return StatusCode(result.httpStatus, result);
        }

    }
}
