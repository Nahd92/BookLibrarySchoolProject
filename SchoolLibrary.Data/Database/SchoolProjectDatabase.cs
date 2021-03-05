using SchoolLibrary.Domain.Models;
using SchoolLibrary.Domain.Models.ModelBooks;
using System.Data.Entity;

namespace SchoolLibrary.Data.Database
{
    public class SchoolProjectDatabase : DbContext
    {
        public SchoolProjectDatabase() : base("SchoolProjectDatabase")
        {
                
        }

        public DbSet<IBooks> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IBooks>()
                           .HasKey(x => new { x.AuthorId, x.CategoryId });
        }
    }
}
