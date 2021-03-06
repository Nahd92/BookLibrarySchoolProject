using SchoolLibrary.Contracts.Request;
using SchoolLibrary.Contracts.Response;
using SchoolLibrary.Domain.Interfaces;
using SchoolLibrary.Domain.Models;
using SchoolLibrary.Domain.Models.ModelBooks;
using SchoolLibrary.Extensions;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SchoolLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly IRepositoryWrapper _repoWrapper;
        public BooksController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }


        [HttpGet]
        public async Task<ActionResult> GetById(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var book = await _repoWrapper.Book.GetBookByIdAsync((int)id);

            if (book == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var Json = new JsonResult { Data = book };
            return new JsonHttpStatusResult(Json.Data, HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var book = await _repoWrapper.Book.GetAllBooksAsync();

            if (book == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var Json = new JsonResult { Data = book };
            return new JsonHttpStatusResult(Json.Data, HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var deleted = await _repoWrapper.Book.DeleteAsync((int)id);

            if (deleted)
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);

            return HttpNotFound();
        }


        [HttpPost]
        public async Task<ActionResult> Create(CreateBookRequest createBookRequest)
        {
            if (!ModelState.IsValid)
                   return new HttpStatusCodeResult(400);


            var author =  await _repoWrapper.Author.CreateAsync(new Author() 
                        { FirstName = createBookRequest.AuthorName, LastName = createBookRequest.AuthorLastName });

            if (author == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No Authors was inputed");


            var category = await _repoWrapper.Category.GetCategoryByName(createBookRequest.Category);

            if (category == null)
                 return new HttpStatusCodeResult(HttpStatusCode.NotFound, "No Category could be found with that name!");
            

            var book = new IBooks
            {
                Title = createBookRequest.Title,
                Descriptions = createBookRequest.Descriptions,
                Published = createBookRequest.Published,
                PageCount = createBookRequest.PageCount,
                ISBN = _repoWrapper.Book.CreateISBN(),
                AuthorId = author.Id,
                CategoryId =  category.Id
            };

            var created = await _repoWrapper.Book.CreateAsync(book);

            if (!created)
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            
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

            var Json =  new JsonResult { Data = response};
            return new JsonHttpStatusResult(Json.Data, HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
        }


        [HttpPut]
        public async Task<ActionResult> Update([Bind(Include = "Id, Title,Author, Description")] int id,  IBooks request)
        {
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var book = new IBooks
            {
                Id = id,
                Title = request.Title,
                Descriptions = request.Descriptions,
                ISBN = request.ISBN
            };

            var updated = await _repoWrapper.Book.UpdateAsync(id, book);

            if (updated)
                return new HttpStatusCodeResult(HttpStatusCode.OK);

            return HttpNotFound();
        }
    }
}