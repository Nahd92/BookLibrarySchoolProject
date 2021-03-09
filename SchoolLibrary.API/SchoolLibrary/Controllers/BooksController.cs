using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SchoolLibrary.Contracts.Request;
using SchoolLibrary.Contracts.Response;
using SchoolLibrary.Contracts.Routes;
using SchoolLibrary.Domain.Interfaces;
using SchoolLibrary.Domain.Models;
using SchoolLibrary.Domain.Models.ModelBooks;
using SchoolLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SchoolLibrary.Controllers
{
    [RoutePrefix("api/Books")]
    public class BooksController : ApiController
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private string ErrorString;

        public BooksController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }


        //Får inte ut bestämda egenskaper från APIn.. Fel SelectTokens?
        [Route(RoutesAPI.Books.GetAllLibraries)]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllLibraries()
        {
            try
            {
                var client = new HttpClient();
                var librariesAPI = await client.GetFromJsonAsync<LibraryAPI>("https://bibdb.libris.kb.se/api/lib?dump=true&country_code=se");

                var returnResponse = new List<LibraryResponse>();

                foreach (var library in librariesAPI.libraries.Where(i => i != null))
                {
                    returnResponse.Add(new LibraryResponse { Name = library.name?.ToString() ?? null, Code = library.country_code?.ToString() ?? null, Url = library.url?.ToString() ?? null });
                };

                return Json(returnResponse);
            }
            catch (Exception ex)
            {
                ErrorString = $"Error: {ex.Message}";
                throw;
            }
        }



        [Route(RoutesAPI.Books.GetById)]
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int? id)
        {
            if (id == null)
                return BadRequest();

            var book = await _repoWrapper.Book.GetBookByIdAsync((int)id);

            if (book == null)
                return NotFound();

            var bookReponse = new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Descriptions = book.Descriptions,
                ISBN = book.ISBN,
                PageCount = book.PageCount,
                Published = book.Published,
                Author = book.AuthorId.ToString(),
                Category = book.CategoryId.ToString()
            };


            return Json(bookReponse);
        }


        [Route(RoutesAPI.Books.GetAll)]
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var book = await _repoWrapper.Book.GetAllBooksAsync();

            if (book == null)
                return NotFound();

          
            return Json(book);
        }


        [Route(RoutesAPI.Books.Delete)]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromUri] int? id)
        {
            if (id == null)
                return BadRequest();

            var deleted = await _repoWrapper.Book.DeleteAsync((int)id);

            if (deleted)
                return Ok();

            return NotFound();
        }


        [Route(RoutesAPI.Books.Create)]
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] CreateBookRequest createBookRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var author = await _repoWrapper.Author.CreateAsync(new Author()
            { FirstName = createBookRequest.AuthorName, LastName = createBookRequest.AuthorLastName });

            if (author == null)
                return BadRequest("No Authors was inputed");


            var category = await _repoWrapper.Category.GetCategoryByName(createBookRequest.Category);

            if (category == null)
                return BadRequest("No Category could be found with that name!");


            var book = new IBooks
            {
                Title = createBookRequest.Title,
                Descriptions = createBookRequest.Descriptions,
                Published = createBookRequest.Published,
                PageCount = createBookRequest.PageCount,
                ISBN = _repoWrapper.Book.CreateISBN(),
                AuthorId = author.Id,
                CategoryId = category.Id
            };

            var created = await _repoWrapper.Book.CreateAsync(book);

            if (!created)
                return BadRequest("Something went wrong when creating a new book");

            var response = new BookResponse
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Descriptions = book.Descriptions,
                Published = book.Published,
                PageCount = book.PageCount,
                Title = book.Title,
                Author = $"{author.FirstName} {author.LastName}",
                Category = category.Name
            };

            return Json(response);
        }


        [Route(RoutesAPI.Books.Update)]
        [HttpPut]
        public async Task<IHttpActionResult> Update([FromUri] int id, [FromBody] UpdateBookRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _repoWrapper.Category.GetCategoryByName(request.Category);

            var book = new IBooks
            {
                Title = request.Title,
                Descriptions = request.Description,
                Published = DateTime.Parse(request.Published),
                PageCount = request.PageCount,
                CategoryId = category.Id
            };

            var updated = await _repoWrapper.Book.UpdateAsync(id, book);

            if (updated)
                return Ok();

            return NotFound();
        }
    }
}