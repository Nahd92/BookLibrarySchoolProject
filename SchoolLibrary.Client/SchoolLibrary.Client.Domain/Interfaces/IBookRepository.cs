using SchoolLibrary.Client.Domain.Models;
using SchoolLibrary.Client.Domain.Requests;
using SchoolLibrary.Client.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.Client.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<BookResponse> GetBookByIdAsync(int id);
        Task<IEnumerable<Books>> GetAllBooksAsync();
        Task<HttpResponseMessage> UpdateAsync(int id, Books book);
        Task<HttpResponseMessage> DeleteAsync(int id);
        Task<HttpResponseMessage> CreateAsync(CreateBooksRequest book);
        Task<List<LibraryResponse>> GetLibraries();

    }
}
