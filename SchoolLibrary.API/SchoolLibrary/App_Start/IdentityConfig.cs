using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SchoolLibrary.Data.Database;
using SchoolLibrary.Domain.Models;

namespace SchoolLibrary
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
        {
            public ApplicationUserStore(SchoolProjectDatabase context) : base(context)
            {
            }
        }
        public class ApplicationUserManager : UserManager<ApplicationUser>
        {
            public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
            {
            }
        }
}