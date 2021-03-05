using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.Domain.Interfaces
{
    public interface IRepositoryWrapper
    {
        IAuthorService Author { get; }
        IBookServices Book { get; }
        ICategoryService Category { get;  }
    }
}
