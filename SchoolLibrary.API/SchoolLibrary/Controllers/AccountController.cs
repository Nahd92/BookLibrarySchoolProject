using Microsoft.AspNet.Identity.EntityFramework;
using SchoolLibrary.Contracts.Request;
using SchoolLibrary.Contracts.Response;
using SchoolLibrary.Contracts.Routes;
using SchoolLibrary.Data.Database;
using SchoolLibrary.Domain.Interfaces;
using SchoolLibrary.Domain.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchoolLibrary.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {

        private readonly IRepositoryWrapper _repoWrapper;
        public AccountController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        // POST api/Account/ChangePassword
        [AllowAnonymous]
        [Route(RoutesAPI.Identity.Register)]       
        public async Task<IHttpActionResult> Register(RegisterUserRequest registerUser) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = registerUser.Email, Email = registerUser.Email };
                var userStore = new UserStore<ApplicationUser>(new SchoolProjectDatabase());
                var userManager = new ApplicationUserManager(userStore);


                var result = await userManager.CreateAsync(user, registerUser.Password);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                RegisterFailedResponse.AddErrors(ModelState, result);
            }

            //Or View if needed back to A page
            return BadRequest("Something went wrong");
        }
    }
}