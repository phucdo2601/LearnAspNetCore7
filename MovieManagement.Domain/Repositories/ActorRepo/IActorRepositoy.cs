using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Repositories.BiographyRepo;
using MovieManagement.Domain.Repositories.Generic;
using MovieManagement.Domain.Repositories.GenreRepo;
using MovieManagement.Domain.Repositories.MovieRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Domain.Repositories.ActorRepo
{
    public interface IActorRepositoy : IGenericRepository<Actor>
    {
        IEnumerable<Actor> GetActorsWithMovies();
    }
}
