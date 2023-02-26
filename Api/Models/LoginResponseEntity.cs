using System;
using online_ordering_api.Models;

namespace online_ordering_api.Models
{
    public class LoginResponseEntity : BaseResponseEntity
    {
        public int? CustomerId { get; set; }

        public string CustomerName { get; set; }

    }
}
