using LearnNet7AuthenAndAuthorB01.Models;
using LearnNet7AuthenAndAuthorB01.Repositories.AccountRepo;
using LearnNet7AuthenAndAuthorB01.Repositories.CategoryRepo;
using LearnNet7AuthenAndAuthorB01.Repositories.ItemRepo;
using LearnNet7AuthenAndAuthorB01.Repositories.RefreshTokenRepo;
using LearnNet7AuthenAndAuthorB01.Repositories.RoleRepo;

namespace LearnNet7AuthenAndAuthorB01.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IAccountRepository AccountRepository => new AccountRepository(_context);

        public IRoleRepository RoleRepository => new RoleRepository(_context);

        public ICategoryRepository CategoryRepository => new CategoryRepository(_context);

        public IItemRepository ItemRepository => new ItemRepository(_context);

        public IRefreshTokenRepository RefreshTokenRepository => new RefreshTokenRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
