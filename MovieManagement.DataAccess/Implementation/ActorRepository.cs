using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Context;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Repositories.ActorRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.DataAccess.Implementation
{
    public class ActorRepository : GenericRepository<Actor>, IActorRepositoy
    {
        private readonly MovieManagementDbContext _context;

        public ActorRepository(MovieManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Actor> GetActorsWithMovies()
        {
            var actorsWithMovie = _context.Actors.Include(a => a.Movies).ToList();
            return actorsWithMovie;
        }
    }
}
