using SchoolLibrary.Contracts.Routes;
using SchoolLibrary.Domain.Interfaces;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchoolLibrary.Controllers
{
    [RoutePrefix("api/Author")]
    public class AuthorController : ApiController
    {
  
        private readonly IRepositoryWrapper _repoWrapper;
        public AuthorController(IRepositoryWrapper repoWrapper) 
        {
            _repoWrapper = repoWrapper;
        }


        [Route(RoutesAPI.Author.GetAll)]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllAuthors()
        {            
            var authors = await _repoWrapper.Author.GetAuthorsAsync();

            if (authors == null)
                return NotFound();

            return Json(authors);
        }

        [Route(RoutesAPI.Author.GetById)]
        [HttpGet]
        public async Task<IHttpActionResult> GetAuthorById(int id)
        {
            var author = await _repoWrapper.Author.GetAuthorByIdAsync(id);

            if (author != null)
                return Json(author);

            return NotFound();
        }











    }
}