using SchoolLibrary.Domain.Models.ModelBooks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolLibrary.Domain.Models
{
    public partial class Author
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


    }
}
