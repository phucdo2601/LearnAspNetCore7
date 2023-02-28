using LearnNet7AuthenAndAuthorB01.Repositories.AccountRepo;
using LearnNet7AuthenAndAuthorB01.Repositories.CategoryRepo;
using LearnNet7AuthenAndAuthorB01.Repositories.ItemRepo;
using LearnNet7AuthenAndAuthorB01.Repositories.RefreshTokenRepo;
using LearnNet7AuthenAndAuthorB01.Repositories.RoleRepo;

namespace LearnNet7AuthenAndAuthorB01.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {

        public int Save();

        IAccountRepository AccountRepository { get; }
        IRoleRepository RoleRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IItemRepository ItemRepository { get; }

        IRefreshTokenRepository RefreshTokenRepository { get; }

    }
}
