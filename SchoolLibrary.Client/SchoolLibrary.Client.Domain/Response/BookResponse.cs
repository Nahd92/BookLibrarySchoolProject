using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.Client.Domain.Response
{
    public class BookResponse
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
