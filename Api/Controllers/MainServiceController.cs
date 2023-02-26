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
    public class MainServiceController : ControllerBase
    {
        private ILogger<MainServiceController> _logger;
        private IMainService _service;

        public MainServiceController(ILogger<MainServiceController> logger, IMainService service)
        {
            _logger = logger;
            _service = service;
        }



        [HttpPost("login")]
        public ActionResult<LoginResponseEntity> LoginService([FromBody] LoginRequestEntity request)
        {

            LoginResponseEntity result = null;
            _logger.LogInformation($"MainServiceController LoginService called... resquest obj : {JsonConvert.SerializeObject(request)}");

            try
            {
                result = _service.LoginService(request);
            }
            catch (Exception ex)
            {
                string logMsg = $"MainServiceController LoginService Error : {ex.Message}";
                Console.WriteLine(logMsg);
                _logger.LogError(logMsg);
            }

            _logger.LogInformation($"MainServiceController LoginService response obj : {JsonConvert.SerializeObject(result)}");
            return StatusCode(result.httpStatus, result);
        }





    }
}
