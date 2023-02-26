using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Configurations;
using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.DataAccess.Context
{
    public class MovieManagementDbContext : DbContext
    {
        public MovieManagementDbContext(DbContextOptions<MovieManagementDbContext> options) : base(options)
        {
            
        }

        #region the region for set dbset for call table on db
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Biography> Biographies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ap dung class config vao DbContext -- Fluent Api
            modelBuilder.ApplyConfiguration(new ActorConfiguration());
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
        }
    }
}
