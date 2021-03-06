using SchoolLibrary.Domain.Interfaces;
using System;

namespace SchoolLibrary.Contracts.Response
{
    public class BookResponse : IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public string ISBN { get; set; }
        public DateTime Published { get; set; }
        public int PageCount { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
    }
}