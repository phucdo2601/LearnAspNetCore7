using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.DataAccess.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                new Movie
                {
                    Id = 1,
                    Name = "Wakanda Forever",
                    Description = "Box Office we coming",
                    ActorId = 1
                },
                new Movie
                {
                    Id = 2,
                    Name = "Ring",
                    Description = "Test film 02",
                    ActorId = 2
                },
                new Movie
                {
                    Id = 3,
                    Name = "Spider Man",
                    Description = "Sky Scapper be warned",
                    ActorId = 1
                },
                new Movie
                {
                    Id = 4,
                    Name = "Matrix",
                    Description = "Blue or Red Pill",
                    ActorId = 3
                }
                );
        }
    }
}
