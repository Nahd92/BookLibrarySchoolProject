using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolLibrary.Client.Domain.Requests
{
    public class CreateBooksRequest
    {
        [Required]
        [Display(Name = "Book Title")]
        public string Title { get; set; }
       
        [Required]
        [Display(Name = "Description")]
        public string Descriptions { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Published year")]
        public DateTime Published { get; set; }
     
        [Required]
        [Display(Name = "Number of pages")]
        public int PageCount { get; set; }

        [Required]
        [Display(Name = "Name of the Category")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Name of the Author FirstName")]
        public string AuthorName { get; set; }

        [Required]
        [Display(Name = "Name of the Authors LastName")]
        public string AuthorLastName { get; set; }
    }
}
