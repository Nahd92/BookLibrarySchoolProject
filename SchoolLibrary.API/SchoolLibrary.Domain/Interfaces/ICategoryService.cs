using SchoolLibrary.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolLibrary.Domain.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> GetCategoryByName(string name);
        Task<bool> UpdateAsync(int id, Category category);
        Task<bool> DeleteAsync(int id);
        Task<bool> CreateAsync(Category category);
    }
}

