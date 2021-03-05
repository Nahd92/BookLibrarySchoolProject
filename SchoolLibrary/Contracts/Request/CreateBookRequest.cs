using SchoolLibrary.Domain.Interfaces;
using System;

namespace SchoolLibrary.Contracts.Request
{
    public class CreateBookRequest
    {
        public int Id { get; set; }   // TAS BORT NÄR MAN KOPPLAR Projectet till databas.
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Published { get; set; }
        public int PageCount { get; set; }
    }
}