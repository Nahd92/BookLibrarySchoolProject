using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolLibrary.Domain.Models
{
    public class LibraryAPI
    {
        public libraries[] libraries { get; set; }
    }
    public class libraries
    {
        [JsonPropertyName("identifier")]
        public string identifier { get; set; }
        [JsonPropertyName("sigel")]
        public string sigel { get; set; }
        public string type { get; set; }
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("dept")]
        public string dept { get; set; }

        [JsonPropertyName("url")]
        public string url { get; set; }
        [JsonPropertyName("county")]
        public object county { get; set; }
        [JsonPropertyName("description")]
        public object description { get; set; }
        [JsonPropertyName("country_code")]
        public string country_code { get; set; }
    }
}