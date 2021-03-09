using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolLibrary.Contracts.Routes
{
    public static class RoutesAPI
    {
        //public const string Root = "api";
        //public const string Version = "v1";
        //public const string Base = Root + "/"; 

        public static class Books
        {
            public const string GetAll = "GetAll";
            public const string Create = "Create";
            public const string GetAllLibraries = "Libraries";
            public const string GetById = "GetById/{id}";
            public const string Update = "Update/{id}";
            public const string Delete = "Delete/{id}";
        }

        public static class Author
        {
            public const string GetAll = "GetAll";
            public const string Create = "Create";
            public const string GetById = "GetById/{id}";
            public const string Update =  "Update/{id}";
            public const string Delete = "Delete/{id}";
        }

        public static class Identity
        {
            public const string Login = "Login";
            public const string Register = "Register";
            public const string Refresh = "Refresh";
        }
    }
}