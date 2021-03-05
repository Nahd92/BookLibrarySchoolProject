using SchoolLibrary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.Domain.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthorById(int id);
        Task<bool> UpdateAsync(int id, Author author);
        Task<bool> DeleteAsync(int id);
        Task<bool> CreateAsync(Author author);
    }
}
