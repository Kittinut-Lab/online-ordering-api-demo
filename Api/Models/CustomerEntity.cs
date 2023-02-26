using System;
using static online_ordering_api.Data.DBColumnExtension;

namespace online_ordering_api.Models
{
    public class CustomerEntity : BaseEntity
    {
        [DbColumn("ID")]
        public int? Id { get; set; }

        [DbColumn("NAME")]
        public string Name { get; set; }

        [DbColumn("EMAIL")]
        public string Email { get; set; }

        [DbColumn("MOBILE_NO")]
        public string MobileNo { get; set; }
    }
}
