using LearnNet7AuthenAndAuthorB01.Models;
using LearnNet7AuthenAndAuthorB01.Repositories.Generic;

namespace LearnNet7AuthenAndAuthorB01.Repositories.RoleRepo
{
    public class RoleRepository : GenericRepositoy<Role>, IRoleRepository
    {
        public RoleRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
