using SchoolLibrary.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolLibrary.Domain.Models.ModelBooks
{
    public class IBooks : IBook
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public DateTime Published { get; set; }
        [Required]
        public int PageCount { get; set; }


        public int AuthorId { get; set; }
        public Author Author { get; set; }
    
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
