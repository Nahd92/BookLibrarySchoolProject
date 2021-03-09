using SchoolLibrary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolLibrary.Contracts.ViewModels
{
    public class BookViewModel
    {
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public DateTime Published { get; set; }
        public int PageCount { get; set; }

        public Category Category { get; set; }
        public Author Author { get; set; }
    }
}