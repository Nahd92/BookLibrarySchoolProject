using SchoolLibrary.Domain.Interfaces;
using System;

namespace SchoolLibrary.Contracts.Response
{
    public class BookResponse : IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public DateTime Published { get; set; }
        public int PageCount { get; set; }
    }
}