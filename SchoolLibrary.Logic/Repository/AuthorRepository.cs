using SchoolLibrary.Data.Database;
using SchoolLibrary.Domain.Interfaces;
using SchoolLibrary.Domain.Models;
using System.Collections.Generic;
using System.Data.Entity;
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


        public async Task<bool> CreateAsync(Author author)
        {
            _database.Authors.Add(author);
            var created = await _database.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await GetAuthorById(id);

            if (author == null)
                return false;

            _database.Authors.Remove(author);
            var deleted = await _database.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Author> GetAuthorById(int id) => await _database.Authors.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Author>> GetAuthors() => await _database.Authors.ToListAsync();

        public async Task<bool> UpdateAsync(int id, Author author)
        {
            var authorId = GetAuthorById(id);
            _database.Entry(authorId).CurrentValues.SetValues(author);
            var updated = await _database.SaveChangesAsync();
            return updated > 0;
        }
    }
}
