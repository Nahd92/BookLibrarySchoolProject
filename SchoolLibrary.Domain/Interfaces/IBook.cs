using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.Domain.Interfaces
{
    public interface IBook
    {

        int Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string ISBN { get; set; }
        DateTime Published { get; set; }
        int PageCount { get; set; }
    }
}
