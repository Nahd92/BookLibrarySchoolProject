using SchoolLibrary.Data.Database;
using SchoolLibrary.Domain.Interfaces;

namespace SchoolLibrary.Logic.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private  SchoolProjectDatabase _database;
        private  IAuthorService _authorService;
        private  IBookServices _bookServices;
        private  ICategoryService _categoryService;


        public IAuthorService Author
        {
            get
            {
                if (_authorService == null)
                {
                    _authorService = new AuthorRepository(_database);
                }
                return _authorService;
            }
        }

        public IBookServices Book
        {
            get
            {
                if (_bookServices == null)
                {
                    _bookServices = new BookRepository(_database);
                }
                return _bookServices;
            }
        }

        public ICategoryService Category
        {
            get
            {
                if (_categoryService == null)
                {
                    _categoryService = new CategoryRepository(_database);
                }
                return _categoryService;
            }
        }

        public RepositoryWrapper(SchoolProjectDatabase database)
        {
            _database = database;
        }
    }
}
