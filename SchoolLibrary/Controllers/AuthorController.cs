using SchoolLibrary.Domain.Interfaces;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SchoolLibrary.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IRepositoryWrapper _repoWrapper;
        public AuthorController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        // GET: Author
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> GetAllAuthors()
        {            
            var authors = await _repoWrapper.Author.GetAuthors();

            if (authors == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return new JsonResult { Data = authors, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public async Task<ActionResult> GetAuthorById(int id)
        {
            var author = await _repoWrapper.Author.GetAuthorById(id);

            if (author != null)
                return new JsonResult { Data = author, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return HttpNotFound();
        }











    }
}