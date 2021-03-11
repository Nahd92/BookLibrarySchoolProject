using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolLibrary.Client.Domain.Models
{
    public class LibraryAPI
    {
        public Libraries[] Libraries { get; set; }
    }
    public class Libraries
    {
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
        [JsonPropertyName("sigel")]
        public string Sigel { get; set; }
        public string Type { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("dept")]
        public string Dept { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("county")]
        public object County { get; set; }
        [JsonPropertyName("description")]
        public object Description { get; set; }
        [JsonPropertyName("country_code")]
        public string Country_code { get; set; }
    }
}