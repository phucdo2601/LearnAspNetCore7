using LearnNet7AuthenAndAuthorB01.Models;
using LearnNet7AuthenAndAuthorB01.Repositories.Generic;

namespace LearnNet7AuthenAndAuthorB01.Repositories.CategoryRepo
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IQueryable<Category> GetAllCatesWithProduct();
    }
}
