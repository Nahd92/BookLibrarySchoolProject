using SchoolLibrary.Domain.Models.ModelBooks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolLibrary.Domain.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


   
    }
}
