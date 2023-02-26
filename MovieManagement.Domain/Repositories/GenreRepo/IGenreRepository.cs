using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Domain.Repositories.GenreRepo
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
    }
}
