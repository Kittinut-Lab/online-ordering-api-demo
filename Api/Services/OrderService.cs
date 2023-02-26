using System;
using System.Transactions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using online_ordering_api.Data;
using online_ordering_api.IService;
using online_ordering_api.Models;

namespace online_ordering_api.Services
{
    public class OrderService : IOrderService
    {
        private IConfiguration _configuration;
        private readonly IDataManager _dataManager;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IConfiguration configuration, IDataManager dataManager, ILogger<OrderService> logger)
        {
            _configuration = configuration;
            _dataManager = dataManager;
            _logger = logger;

        }

        public BaseResponseEntity ConfirmOrderService(RequestConfirmOrderEntity request)
        {
            BaseResponseEntity result = new BaseResponseEntity();
            var jsonsRes = string.Empty;
            try
            {
                if (request == null)
                {
                    result.msg = "request is null";
                    result.code = "ER-01";
                    return result;
                }
                if (request.CustomerID == null)
                {
                    result.msg = "request CustomerID is null";
                    result.code = "ER-02";
                    return result;
                }
                if (request.ProductID == null)
                {
                    result.msg = "request ProductID is null";
                    result.code = "ER-03";
                    return result;
                }

                // * Calculate order price.
                var createdOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var createdBy = request.CustomerID;
                var updatedOn =  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var updatedBy = request.CustomerID;
                var orderPrice = (request.Amount * request.ProductPrice);
                var productStatus = 1; // * pending status.

                string query = $@"INSERT INTO SALE_ORDER(AMOUNT,PRODUCT_PRICE,PRODUCT_NAME,PRODUCT_CODE,ORDER_PRICE,STATUS,CREATED_ON,CREATED_BY,UPDATED_ON,UPDATED_BY) 
                                values(
                                    {request.Amount},
                                    {request.ProductPrice},
                                    N'{request.ProductName}',
                                    {request.ProductID},
                                    {orderPrice},
                                    {productStatus},
                                    '{createdOn}',
                                    {createdBy},
                                    '{updatedOn}',
                                    {updatedBy}
                                     )";

                using (TransactionScope scope = new TransactionScope())
                {
                    _dataManager.ExecuteNoneQuery(query);
                    scope.Complete();
                }


                result.code = "BR-XX-XX00";
                result.msg = "Success";
                result.httpStatus = 200;


            }
            catch (Exception ex)
            {
                result.code = "ER-999";
                result.msg = $"ConfirmOrderService Error : {ex.Message}";
                _logger.LogError($"{result.msg} :  jsons response : {jsonsRes}");

            }
            return result;
        }
    }
}
