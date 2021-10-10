using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace obfuscation_utility
{
   public class CountryObj
    {
        [JsonProperty("timezones")]
        public List<string> Timezone { get; set; }
        [JsonProperty("latlng")]
        public List<double> Lating { get; set; }

       
        [JsonProperty("name")]
        public string CountryName { get; set; }
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        [JsonProperty("capital")]
        public string CountryCapital { get; set; }
       
    }
}
