using Newtonsoft.Json;
using SchoolLibrary.Client.Domain.Models;
using SchoolLibrary.Client.Domain.Requests;
using SchoolLibrary.Client.Domain.Response;
using SchoolLibrary.Client.Logic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace SchoolLibrary.Client.Controllers
{
    [Route("[controller]/[action]")]
    public class BooksController : Controller
    {
        private readonly BookRepository _bookRepo;
        public BooksController(BookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }


        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public async Task<ActionResult> Index()
        {
            var allBooks = await _bookRepo.GetAllBooksAsync();
            if (allBooks == null)
                return View();

             return View(allBooks);
        }

        [HttpGet]
        [Route("Books/Details/{id:int}")]
        public async Task<ActionResult> Details(int id)
        {
            var book = await _bookRepo.GetBookByIdAsync(id);
            if (book == null)
                return View();

            return View(book);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateBooksRequest();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBooksRequest request)
        {
            if (!ModelState.IsValid)
                return View();

           var response = await _bookRepo.CreateAsync(request);

            if (response.IsSuccessStatusCode)
                    return View("SuccessfullyCreatedBook"); 

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            var book = await _bookRepo.GetBookByIdAsync((int)id);
            if (book == null)
                return View();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _bookRepo.DeleteAsync(id);

            if (response.IsSuccessStatusCode)
                return View("SuccessfullyDeletedBook");

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Libraries()
        {
            var allLibraries = await _bookRepo.GetLibraries();
            if (allLibraries == null)
                return View();

            return View(allLibraries);
        }
    }
}