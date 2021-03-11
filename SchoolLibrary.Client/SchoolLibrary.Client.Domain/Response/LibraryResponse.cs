using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.Client.Domain.Response
{
   public class LibraryResponse
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public string Dept { get; set; }
        public string County { get; set; }

    }
}
