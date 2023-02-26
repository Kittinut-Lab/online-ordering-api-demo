using System;
using static online_ordering_api.Data.DBColumnExtension;

namespace online_ordering_api.Models
{
    public class BaseEntity
    {
        [DbColumn("CREATED_ON")]
        public DateTime? CreatedOn { get; set; }

        [DbColumn("CREATED_BY")]
        public int? CreatedBy { get; set; }

        [DbColumn("UPDATED_ON")]
        public DateTime? UpdatedOn { get; set; }

        [DbColumn("UPDATED_BY")]
        public int? UpdatedBy { get; set; }
    }
}
