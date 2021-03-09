using SchoolLibrary.Data.Database;
using SchoolLibrary.Domain.Interfaces;
using SchoolLibrary.Domain.Models.ModelBooks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolLibrary.Logic.Repository
{
    public class BookRepository : IBookServices
    {
        private readonly SchoolProjectDatabase _database;

        public BookRepository(SchoolProjectDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<IBooks>> GetAllBooksAsync() => await _database.Books.ToListAsync();

        public async Task<IBooks> GetBookByIdAsync(int id) => await _database.Books.FirstOrDefaultAsync(x => x.Id == id);


        public async Task<bool> CreateAsync(IBooks books)
        {
            _database.Books.Add(books);
            var created = await _database.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await GetBookByIdAsync(id);

            if (book == null)
                return false;
         
             _database.Books.Remove(book);
             var deleted = await _database.SaveChangesAsync();
             return deleted > 0;
        }


        //Oklart om denna fungererar
        public async Task<bool> UpdateAsync(int id, IBooks books)
        {
            try
            {
                IBooks book = await _database.Books.Where(x => x.Id == id).SingleOrDefaultAsync();

                books.Id = book.Id;
                books.ISBN = book.ISBN;
                books.AuthorId = book.AuthorId;

                _database.Entry(book).CurrentValues.SetValues(books);
                var updated = await _database.SaveChangesAsync();
                return updated > 0;
            }
            catch (DbEntityValidationException dbex)
            {
                foreach (var validationErrors in dbex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            return false;
        }


        public string CreateISBN()
        {
            var result = new int[13]; 
            var random = new Random();
            string resultString;
            do
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = random.Next(0, 9);
                };
                resultString = string.Join("", result);
            } while (ISBNExists(resultString));
                 
            return resultString;
        }

        public bool ISBNExists(string reference) => _database.Books.Any(x => x.ISBN == reference);



        public async Task<IEnumerable<IBooks>> GetBooksByCategory(int categoryId) => 
                                   await _database.Books.Include(x => x.Category)
                                                    .Where(y => y.CategoryId == categoryId).ToListAsync();

        public async Task<IEnumerable<IBooks>> GetBookByAuthor(string authorsName) =>
                                    await _database.Books.Where(x => x.Author.FirstName.Contains(authorsName) ||
                                                    x.Author.LastName.Contains(authorsName)).ToListAsync();

    }

}
