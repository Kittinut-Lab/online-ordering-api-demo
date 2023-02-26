using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using online_ordering_api.Data;
using online_ordering_api.IService;
using online_ordering_api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace online_ordering_api.Services
{
    public class MainService : IMainService
    {

        private IConfiguration _configuration;
        private readonly IDataManager _dataManager;
        private readonly ILogger<MainService> _logger;

        public string GATEWAY_URI { get; }

        public MainService(IConfiguration configuration, IDataManager dataManager, ILogger<MainService> logger)
        {
            _configuration = configuration;
            _dataManager = dataManager;
            _logger = logger;

        }

        public LoginResponseEntity LoginService(LoginRequestEntity request)
        {
            LoginResponseEntity result = new LoginResponseEntity();
            var jsonsRes = string.Empty;
            try
            {
                if (request == null)
                {
                    result.msg = "request is null";
                    result.code = "ER-01";
                    return result;
                }
                if (string.IsNullOrEmpty(request.email))
                {
                    result.msg = "request email is null";
                    result.code = "ER-02";
                    return result;
                }
                if (string.IsNullOrEmpty(request.mobileNo))
                {
                    result.msg = "request mobile is null";
                    result.code = "ER-03";
                    return result;
                }

                //Check customer data.
                string query = $@"SELECT * FROM [CUSTOMER]
                                WHERE CUSTOMER.EMAIL = N'{request.email}'
                                AND CUSTOMER.MOBILE_NO = N'{request.mobileNo}'";

                var data = _dataManager.ExecuteReaderData<CustomerEntity>(query);

                if (data == null)
                {
                    result.code = "BR-01-0401";
                    result.msg = "invalid username or password.";
                    result.httpStatus = 200;
                    return result;
                }

                result.CustomerId = data.Id;
                result.CÏustomerName = data.Name;
                result.code = "BR-XX-XX00";
                result.msg = "Success";
                result.httpStatus = 200;


            }
            catch (Exception ex)
            {
                result.code = "ER-999";
                result.msg = $"LoginService Error : {ex.Message}";
                _logger.LogError($"{result.msg} :  jsons response : {jsonsRes}");



            }
            return result;
        }
    }
}
