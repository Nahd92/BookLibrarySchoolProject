using Newtonsoft.Json;
using SchoolLibrary.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolLibrary.Contracts.Request
{
    public class CreateBookRequest
    {
        [JsonIgnore]
        public int Id { get; set; }

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