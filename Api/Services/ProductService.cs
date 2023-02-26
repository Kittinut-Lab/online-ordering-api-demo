using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using online_ordering_api.Data;
using online_ordering_api.IService;
using online_ordering_api.Models;

namespace online_ordering_api.Services
{
    public class ProductService : IProductService
    {
        private IConfiguration _configuration;
        private readonly IDataManager _dataManager;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IConfiguration configuration, IDataManager dataManager, ILogger<ProductService> logger)
        {
            _configuration = configuration;
            _dataManager = dataManager;
            _logger = logger;

        }

        public ListProductsResponseEntity ListProductService()
        {
            ListProductsResponseEntity result = new ListProductsResponseEntity();
            var jsonsRes = string.Empty;
            try
            {

                //Check customer data.
                string query = $@"SELECT * FROM [PRODUCT]";
                var data = _dataManager.ExecuteReaderDataSet<ProductEntity>(query);

                if (data == null || data.Count == 0)
                {
                    result.code = "BR-01-0401";
                    result.msg = "Customer not found.";
                    result.httpStatus = 404;
                    return result;
                }

                result.Products = data;
                result.code = "BR-XX-XX00";
                result.msg = "Success";
                result.httpStatus = 200;

            }
            catch (Exception ex)
            {
                result.code = "ER-999";
                result.msg = $"ListProductService Error : {ex.Message}";
                _logger.LogError($"{result.msg} :  jsons response : {jsonsRes}");

            }
            return result;
        }
    }
}
