using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SchoolLibrary.Controllers
{
    public class HomeController : ApiController
    {
        // GET: Home
            public IHttpActionResult Index()
            {
                string welcome = "Hello mister"; 

                return Ok(welcome);
            }
        }
}