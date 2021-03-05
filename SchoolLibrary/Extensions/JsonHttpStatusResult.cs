using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SchoolLibrary.Extensions
{
    public class JsonHttpStatusResult : JsonResult
    {
        public readonly int StatusCode;

        public JsonHttpStatusResult(object data, HttpStatusCode httpStatus)
        {
            Data = data;
            StatusCode = Convert.ToInt32(httpStatus);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = (int)StatusCode;
            base.ExecuteResult(context);
        }
    }
}