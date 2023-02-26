using System;
using System.Text.Json.Serialization;

namespace online_ordering_api.Models
{
    public class BaseResponseEntity
    {
        public BaseResponseEntity()
        {
        }
        [JsonIgnore]
        public int httpStatus { get; set; }
        [JsonIgnore]
        public bool IsError => (!(this.msg == "ok" || this.msg =="Success")) ? true : false;
        public string code { get; set; }
        public string msg { get; set; }

    }
}
