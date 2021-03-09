using SchoolLibrary.Client.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SchoolLibrary.Client.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var books = await client.GetFromJsonAsync<IEnumerable<Authors>>("https://localhost:44382/api/Author/GetAll");
                return View(books);
            }
        }
    }
}