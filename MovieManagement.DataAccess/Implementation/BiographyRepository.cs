using MovieManagement.DataAccess.Context;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Repositories.BiographyRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.DataAccess.Implementation
{
    public class BiographyRepository : GenericRepository<Biography>, IBiographyRepository
    {
        public BiographyRepository(MovieManagementDbContext context) : base(context)
        {

        }
    }
}
