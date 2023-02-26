using MovieManagement.Domain.Repositories.ActorRepo;
using MovieManagement.Domain.Repositories.BiographyRepo;
using MovieManagement.Domain.Repositories.GenreRepo;
using MovieManagement.Domain.Repositories.MovieRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Domain.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IActorRepositoy ActorRepositoy { get; }
        IMovieRepository MovieRepository { get; }
        IGenreRepository GenreRepository { get; }
        IBiographyRepository BiographyRepository { get; }

        int save();
    }
}
