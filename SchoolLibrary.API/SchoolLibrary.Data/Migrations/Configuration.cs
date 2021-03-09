namespace SchoolLibrary.Data.Migrations
{
    using SchoolLibrary.Domain.Models;
    using SchoolLibrary.Domain.Models.ModelBooks;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolLibrary.Data.Database.SchoolProjectDatabase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchoolLibrary.Data.Database.SchoolProjectDatabase context)
        {

            context.Categories.AddOrUpdate(x => x.Id,
                        new Category() { Id = 1, Name = "Adventure" },
                        new Category() { Id = 2, Name = "Baby" },
                        new Category() { Id = 3, Name = "Fanstasy" },
                        new Category() { Id = 4, Name = "Thriller" });

        }
    }
}
