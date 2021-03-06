using SchoolLibrary.Data.Database;
using SchoolLibrary.Domain.Interfaces;
using SchoolLibrary.Domain.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolLibrary.Logic.Repository
{
    public class AuthorRepository : IAuthorService
    {
        private readonly SchoolProjectDatabase _database;
        public AuthorRepository(SchoolProjectDatabase database)
        {
            _database = database;
        }
        public async Task<Author> GetAuthorByName(Author author) =>
                        await _database.Authors.Where(x => x.FirstName == author.FirstName 
                                        && x.LastName == author.LastName).FirstOrDefaultAsync();

        public bool AuthorExistByName(string firstName, string LastName) => 
                    _database.Authors.Any(x => x.FirstName == firstName && x.LastName == LastName);

        public async Task<Author> CreateAsync(Author author)
        {
            if (AuthorExistByName(author.FirstName, author.LastName)) 
                return await GetAuthorByName(author);
            
            var createdAuthor = _database.Authors.Add(author);
            await _database.SaveChangesAsync();
            return createdAuthor;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await GetAuthorByIdAsync(id);

            if (author == null)
                return false;

            _database.Authors.Remove(author);
            var deleted = await _database.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Author> GetAuthorByIdAsync(int id) => await _database.Authors.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Author>> GetAuthorsAsync() => await _database.Authors.ToListAsync();

        public async Task<bool> UpdateAsync(int id, Author author)
        {
            var authorId = GetAuthorByIdAsync(id);
            _database.Entry(authorId).CurrentValues.SetValues(author);
            var updated = await _database.SaveChangesAsync();
            return updated > 0;
        }

     
    }
}
