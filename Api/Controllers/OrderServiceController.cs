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
    public class OrderServiceController : ControllerBase
    {
        private ILogger<OrderServiceController> _logger;
        private IOrderService _service;

        public OrderServiceController(ILogger<OrderServiceController> logger, IOrderService service)
        {
            _logger = logger;
            _service = service;
        }



        [HttpPost("confirmOrder")]
        public ActionResult<BaseResponseEntity> ConfirmOrderService([FromBody] RequestConfirmOrderEntity request)
        {

            BaseResponseEntity result = null;
            _logger.LogInformation($"OrderServiceController ConfirmOrderService called... resquest obj : {JsonConvert.SerializeObject(request)}");

            try
            {
                result = _service.ConfirmOrderService(request);
            }
            catch (Exception ex)
            {
                string logMsg = $"OrderServiceController ConfirmOrderService Error : {ex.Message}";
                Console.WriteLine(logMsg);
                _logger.LogError(logMsg);
            }

            _logger.LogInformation($"OrderServiceController ConfirmOrderService response obj : {JsonConvert.SerializeObject(result)}");
            return StatusCode(result.httpStatus, result);
        }





    }
}
