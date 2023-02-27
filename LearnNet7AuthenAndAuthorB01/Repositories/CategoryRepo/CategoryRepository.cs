using LearnNet7AuthenAndAuthorB01.Models;
using LearnNet7AuthenAndAuthorB01.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace LearnNet7AuthenAndAuthorB01.Repositories.CategoryRepo
{
    public class CategoryRepository : GenericRepositoy<Category>, ICategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context) : base(context)
        {
            _context= context;
        }

        public IQueryable<Category> GetAllCatesWithProduct()
        {
            var result = _context.Categories.Include(c => c.Items);
            return result;
        }
    }
}
