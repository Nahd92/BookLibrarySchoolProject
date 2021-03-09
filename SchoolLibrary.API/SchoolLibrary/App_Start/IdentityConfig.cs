using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SchoolLibrary.Data.Database;
using SchoolLibrary.Domain.Models;

namespace SchoolLibrary
{
    //public class ApplicationUserStore : UserStore<ApplicationUser>
    //    {
    //        public ApplicationUserStore(SchoolProjectDatabase context) : base(context)
    //        {
    //        }
    //    }


    //    public class ApplicationUserManager : UserManager<ApplicationUser>
    //    {
    //        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
    //        {
            
    //        }

    //        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    //        {
    //        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<SchoolProjectDatabase>()));

    //            manager.PasswordValidator = new PasswordValidator
    //            {
    //                RequiredLength = 5,
    //                RequireLowercase = false,
    //                RequireUppercase = false,
    //                RequireNonLetterOrDigit = false,
    //                RequireDigit = true,
    //            };

    //         return manager;
    //        }
    //    }



    //    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    //    {
    //    public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager):
    //                        base(userManager, authenticationManager)
    //            {
    //            }
    //    public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
    //    {
    //        return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    //    }
    //}
}