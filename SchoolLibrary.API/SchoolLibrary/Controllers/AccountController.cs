//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
//using SchoolLibrary.Contracts.Request;
//using SchoolLibrary.Contracts.Response;
//using SchoolLibrary.Contracts.Routes;
//using SchoolLibrary.Data.Database;
//using SchoolLibrary.Domain.Interfaces;
//using SchoolLibrary.Domain.Models;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;

//namespace SchoolLibrary.Controllers
//{
//    [RoutePrefix("api/Account")]
//    public class AccountController : ApiController
//    {
//        private ApplicationUserManager _userManager;
//        private ApplicationSignInManager _signInManager;
//        private readonly IRepositoryWrapper _repoWrapper;
//        public AccountController(ApplicationUserManager userManager, 
//            ApplicationSignInManager signInManager, IRepositoryWrapper repoWrapper)
//        {
//            _repoWrapper = repoWrapper;
//            var userStore = new UserStore<ApplicationUser>(new SchoolProjectDatabase());
//            _userManager = new ApplicationUserManager(userStore);
//            _signInManager = signInManager;
//        }

//        // POST api/Account/Register
//        [HttpPost]
//        [AllowAnonymous]
//        [Route(RoutesAPI.Identity.Register)] 
//        public async Task<IHttpActionResult> Register([FromBody] RegisterUserRequest registerUser) 
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (ModelState.IsValid)
//            {
//                var user = new ApplicationUser() { UserName = registerUser.Email, Email = registerUser.Email };

//                var result = await _userManager.CreateAsync(user, registerUser.Password);
//                if (result.Succeeded)
//                {
//                    return Ok(result);
//                }
//                RegisterFailedResponse.AddErrors(ModelState, result);
//            }

//            //Or View if needed back to A page
//            return BadRequest("Something went wrong");
//        }


//        [HttpPost]
//        [AllowAnonymous]
//        [Route(RoutesAPI.Identity.Login)]
//        public async Task<IHttpActionResult> Login([FromBody] UserLoginRequest userRequest)
//        {
//            var user = await _userManager.FindByEmailAsync(userRequest.Email);

//            if (user != null)
//                await _signInManager.PasswordSignInAsync(userRequest.Email, userRequest.Password, false, false);
//            return Ok();    
//        }


//        [HttpPost]
//        [AllowAnonymous]
//        [Route(RoutesAPI.Identity.Logout)]
//        public IHttpActionResult Logout()
//        {
//            var AuthenticationManager = HttpContext.Current.GetOwinContext().Authentication;
//             AuthenticationManager.SignOut();
//            return Ok();
//        }
//    }
//}