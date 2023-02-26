using System;
using static online_ordering_api.Data.DBColumnExtension;

namespace online_ordering_api.Models
{
    public class ProductEntity : BaseEntity
    {
        [DbColumn("ID")]
        public int? Id { get; set; }

        [DbColumn("NAME")]
        public string Name { get; set; }

        [DbColumn("CODE")]
        public string Code { get; set; }

        [DbColumn("PRICE")]
        public double Price { get; set; }

        [DbColumn("IMAGE")]
        public string Image { get; set; }

        [DbColumn("STATUS")]
        public bool Status { get; set; }

        [DbColumn("DESC")]
        public string Desc { get; set; }
    }
}
