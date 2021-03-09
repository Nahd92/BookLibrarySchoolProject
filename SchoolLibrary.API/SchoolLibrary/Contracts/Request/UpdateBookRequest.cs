using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolLibrary.Contracts.Request
{
    public class UpdateBookRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Published { get; set; }
        public int PageCount { get; set; }
        public string Category { get; set; }
    }
}