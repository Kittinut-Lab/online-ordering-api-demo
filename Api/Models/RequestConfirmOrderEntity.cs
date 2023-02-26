using System;
namespace online_ordering_api.Models
{
    public class RequestConfirmOrderEntity
    {
        public int? CustomerID { get; set; }
        public int? ProductID { get; set; }
        public int? Amount { get; set; }
        public double? ProductPrice { get; set; }
        public string ProductName { get; set; }
    }
}
