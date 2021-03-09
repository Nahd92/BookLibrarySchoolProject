using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolLibrary.Contracts.Response
{
    public class LibraryResponse
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
    }
}
