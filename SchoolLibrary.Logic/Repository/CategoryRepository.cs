using SchoolLibrary.Data.Database;
using SchoolLibrary.Domain.Interfaces;
using SchoolLibrary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolLibrary.Logic.Repository
{
    public class CategoryRepository : ICategoryService
    {
        private readonly SchoolProjectDatabase _database;

        public CategoryRepository(SchoolProjectDatabase database)
        {
            _database = database;
        }

        public async Task<bool> CreateAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await GetCategoryById(id);

            if (category == null)
                return false;

            _database.Categories.Remove(category);
            var deleted = await _database.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<IEnumerable<Category>> GetCategories() => await _database.Categories.ToListAsync();

        public async Task<Category> GetCategoryById(int id) => 
                        await _database.Categories.Where(x => x.Id == id).SingleOrDefaultAsync();

        public async Task<Category> GetCategoryByName(string name) => 
                        await _database.Categories.Where(x => x.Name == name).SingleOrDefaultAsync();


        public async Task<bool> UpdateAsync(int id, Category category)
        {
            throw new NotImplementedException();
        }
    }
}
