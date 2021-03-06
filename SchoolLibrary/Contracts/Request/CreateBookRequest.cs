using SchoolLibrary.Domain.Interfaces;
using System;

namespace SchoolLibrary.Contracts.Request
{
    public class CreateBookRequest
    {
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public DateTime Published { get; set; }
        public int PageCount { get; set; }

        public string Category { get; set; }
        public string AuthorName { get; set; }
        public string AuthorLastName { get; set; }
    }
  
}