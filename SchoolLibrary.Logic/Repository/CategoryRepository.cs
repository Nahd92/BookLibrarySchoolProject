using SchoolLibrary.Data.Database;
using SchoolLibrary.Domain.Interfaces;
using SchoolLibrary.Domain.Models;
using System;
using System.Collections.Generic;
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

        public Task<bool> CreateAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, Category category)
        {
            throw new NotImplementedException();
        }
    }
}
