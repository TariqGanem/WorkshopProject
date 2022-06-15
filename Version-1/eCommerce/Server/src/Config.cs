using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce
{
    public class Config
    {
        [JsonProperty("dbenv")]
        public String dbenv { get; set; }

        [JsonProperty("email")]
        public String email { get; set; }

        [JsonProperty("password")]
        public String password { get; set; }

        [JsonProperty("mongoDB_url")]
        public String mongoDB_url { get; set; }

        [JsonProperty("externalSystem_url")]
        public String externalSystem_url { get; set; }
    }
}
