using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnNet7AuthenAndAuthorB01.Models.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);

            //lien ket khao ngoai CategoryId cua bang Item den bang Category
            builder.HasOne(x => x.Category).WithMany(x => x.Items).HasForeignKey(x => x.CategoryId);
        }
    }
}
