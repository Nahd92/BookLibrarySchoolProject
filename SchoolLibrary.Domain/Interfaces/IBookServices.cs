
using SchoolLibrary.Domain.Models.ModelBooks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolLibrary.Domain.Interfaces
{
    public interface IBookServices
    {
        Task<IBooks> GetBookByIdAsync(int id);
        Task<IEnumerable<IBooks>> GetAllBooksAsync();
        Task<bool> UpdateAsync(int id, IBooks book);
        Task<bool> DeleteAsync(int id);
        Task<bool> CreateAsync(IBooks book);
        string CreateISBN();
        bool ISBNExists(string reference);
        Task<IEnumerable<IBooks>> GetBooksByCategory(int categoryId);
        Task<IEnumerable<IBooks>> GetBookByAuthor(string authorsName);
    }
}
