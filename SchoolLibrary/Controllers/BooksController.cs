using SchoolLibrary.Contracts.Request;
using SchoolLibrary.Contracts.Response;
using SchoolLibrary.Domain.Interfaces;
using SchoolLibrary.Domain.Models.ModelBooks;
using SchoolLibrary.Extensions;
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

            var Json = new JsonResult { Data = book, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return new JsonHttpStatusResult(Json.Data, HttpStatusCode.OK);
        }


        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var book = await _repoWrapper.Book.GetAllBooksAsync();

            if (book == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var Json = new JsonResult { Data = book, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return new JsonHttpStatusResult(Json.Data, HttpStatusCode.OK);
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
        public async Task<ActionResult> Create([Bind(Include = "")] CreateBookRequest createBookRequest)
        {
            if (!ModelState.IsValid)
                   return new HttpStatusCodeResult(400);
            
            var book = new IBooks
            {
                Id = createBookRequest.Id, // <--- Tas bort när projeket bind med Databas
                Title = createBookRequest.Title,
                Description = createBookRequest.Description,
                Published = createBookRequest.Published,
                PageCount = createBookRequest.PageCount,
                ISBN = _repoWrapper.Book.CreateISBN(),               
            };

            var created = await _repoWrapper.Book.CreateAsync(book);

            // if (!created)
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var response = new BookResponse
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Description = book.Description,
                Published = book.Published,
                PageCount = book.PageCount
            };

            var Json =  new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet   };
            return new JsonHttpStatusResult(Json.Data, HttpStatusCode.Created);
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
                Description = request.Description,
                ISBN = request.ISBN
            };

            var updated = await _repoWrapper.Book.UpdateAsync(id, book);

            if (updated)
                return new HttpStatusCodeResult(HttpStatusCode.OK);

            return HttpNotFound();
        }
    }
}