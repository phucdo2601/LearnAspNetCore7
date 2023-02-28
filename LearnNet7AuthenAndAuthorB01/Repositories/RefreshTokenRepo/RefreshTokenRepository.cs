using LearnNet7AuthenAndAuthorB01.Models;
using LearnNet7AuthenAndAuthorB01.Repositories.Generic;

namespace LearnNet7AuthenAndAuthorB01.Repositories.RefreshTokenRepo
{
    public class RefreshTokenRepository : GenericRepositoy<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DatabaseContext context) : base(context) 
        {

        }
    }
}
