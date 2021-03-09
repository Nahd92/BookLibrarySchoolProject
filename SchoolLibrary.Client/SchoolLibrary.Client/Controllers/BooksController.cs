using Newtonsoft.Json;
using SchoolLibrary.Client.Domain.Models;
using SchoolLibrary.Client.Domain.Requests;
using SchoolLibrary.Client.Domain.Response;
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
        // GET: Books


        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var books = await client.GetFromJsonAsync<IEnumerable<Books>>("https://localhost:44382/api/Books/getAll");
                 return View(books);             
            }
       }

        [HttpGet]
        [Route("Books/Details/{id:int}")]
        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                var path = "https://localhost:44382/api/Books/GetById";
                var book = await client.GetFromJsonAsync<BookResponse>(path + $"/{id}");
                return View(book);
            }
        }



        [HttpGet]

        public ActionResult Create()
        {
            var book = new CreateBooksRequest();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBooksRequest request)
        {
            
            using (var client = new HttpClient())
            {
                var bookRequest = JsonConvert.SerializeObject(request);
                HttpContent content = new StringContent(bookRequest, Encoding.UTF8, "application/json");

                var result = await client.PostAsync("https://localhost:44382/api/Books/Create", content);
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }

            return View(request);         
        }


        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            using (var client = new HttpClient())
            {
                var path = "https://localhost:44382/api/Books/GetById";
                var response = await client.GetFromJsonAsync<Books>(path + $"/{id}");
                return View(response);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync("https://localhost:44382/api/Books/Delete" + $"/{id}");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            return View(id);
        }
    }
}