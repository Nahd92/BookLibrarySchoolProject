using Newtonsoft.Json;
using SchoolLibrary.Client.Domain.Interfaces;
using SchoolLibrary.Client.Domain.Models;
using SchoolLibrary.Client.Domain.Requests;
using SchoolLibrary.Client.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.Client.Logic.Repository
{
   public class BookRepository : IBookRepository
    {
        private readonly IHttpClientProvider _httpProvider;
        // private readonly HttpClient _httpProvider;
        public BookRepository(IHttpClientProvider httpProvider)
        {
            _httpProvider = httpProvider;
        }

        public async Task<HttpResponseMessage> CreateAsync(CreateBooksRequest request)
        { 
            var bookRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(bookRequest, Encoding.UTF8, "application/json");
            var requestUrl = "https://localhost:44382/api/Books/Create";
            var result = await _httpProvider.PostAsync(requestUrl, content);
            return result;
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var requestUrl = "https://localhost:44382/api/Books/Delete" + $"/{id}";
            var result = await _httpProvider.DeleteAsync(requestUrl);
            return result;
        }

        public async Task<IEnumerable<Books>> GetAllBooksAsync()
        {
            var requestUrl = "https://localhost:44382/api/Books/GetAll";
            var response = await _httpProvider.GetAsync(requestUrl);

            var responseData = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<IEnumerable<Books>>(responseData);

            return books;
        }

        public async Task<BookResponse> GetBookByIdAsync(int id)
        {
            var requestUrl = "https://localhost:44382/api/Books/GetById";
            var response = await _httpProvider.GetAsync(requestUrl + $"/{id}");

            var responseData = await response.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<BookResponse>(responseData);

            return book;
        }

        public async Task<List<LibraryResponse>> GetLibraries()
        {
            var requestUrl = "https://bibdb.libris.kb.se/api/lib?dump=true&country_code=se";
            var response = await _httpProvider.GetAsync(requestUrl);

            var responseData = await response.Content.ReadAsStringAsync();
            var librariesAPI = JsonConvert.DeserializeObject<LibraryAPI>(responseData);

            var returnResponse = new List<LibraryResponse>();

                foreach (var library in librariesAPI.Libraries.Where(i => i != null))
                 {
                  returnResponse.Add(new LibraryResponse
                  {
                   Name = library.Name.ToString() ?? null,
                   Code = library.Country_code?.ToString() ?? null,
                   Url = library.Url?.ToString() ?? null,
                   Dept = library.Dept?.ToString() ?? null,
                   County = library.County?.ToString() ?? null
                  });              
                }
            return returnResponse;
         }   




        public Task<HttpResponseMessage> UpdateAsync(int id, Books book)
        {
            throw new NotImplementedException();
        }



    }
}
