using System;
using static online_ordering_api.Data.DBColumnExtension;

namespace online_ordering_api.Models
{
    public class SaleOrderEntiy : BaseEntity
    {
        [DbColumn("ID")]
        public int? Id { get; set; }

        [DbColumn("AMOUNT")]
        public int Amount { get; set; }

        [DbColumn("PRODUCT_PRICE")]
        public double ProductPrice { get; set; }

        [DbColumn("PRODUCT_NAME")]
        public string ProductName { get; set; }

        [DbColumn("PRODUCT_CODE")]
        public string ProductCode { get; set; }

        [DbColumn("ORDER_PRICE")]
        public double OrderPrice { get; set; }

        [DbColumn("STATUS")]
        public double Status { get; set; }


    }
}
