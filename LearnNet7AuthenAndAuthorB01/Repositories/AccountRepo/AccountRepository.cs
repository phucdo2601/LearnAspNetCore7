using LearnNet7AuthenAndAuthorB01.Models;
using LearnNet7AuthenAndAuthorB01.Repositories.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace LearnNet7AuthenAndAuthorB01.Repositories.AccountRepo
{
    public class AccountRepository : GenericRepositoy<Account>, IAccountRepository
    {
        public AccountRepository(DatabaseContext context) : base(context) 
        {

        }
    }
}
