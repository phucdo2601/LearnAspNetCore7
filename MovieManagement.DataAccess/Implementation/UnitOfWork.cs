using MovieManagement.DataAccess.Context;
using MovieManagement.Domain.Repositories.ActorRepo;
using MovieManagement.Domain.Repositories.BiographyRepo;
using MovieManagement.Domain.Repositories.GenreRepo;
using MovieManagement.Domain.Repositories.MovieRepo;
using MovieManagement.Domain.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieManagementDbContext _context;

        public UnitOfWork(MovieManagementDbContext context)
        {
            _context= context;
        }

        public IActorRepositoy ActorRepositoy => new ActorRepository(_context);

        public IMovieRepository MovieRepository =>  new MovieRepository(_context);

        public IGenreRepository GenreRepository => new GenreRepository(_context);

        public IBiographyRepository BiographyRepository =>  new BiographyRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public int save()
        {
            return _context.SaveChanges();  
        }
    }
}
