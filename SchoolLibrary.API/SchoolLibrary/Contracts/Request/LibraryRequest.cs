using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace SchoolLibrary.Contracts.Request
{
    public class LibraryRequest
    {
      
        public string Name { get; set; }

        public string Code { get; set; }
        
        public string Url { get; set; }
    }
}