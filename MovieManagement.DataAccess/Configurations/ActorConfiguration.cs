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
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(
                new Actor
                {
                    Id = 1, FirstName ="Chunk", LastName = "Norris"
                },
                new Actor
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe"
                },
                new Actor
                {
                    Id = 3,
                    FirstName = "Van",
                    LastName = "Dame"
                }
                );
        }
    }
}
