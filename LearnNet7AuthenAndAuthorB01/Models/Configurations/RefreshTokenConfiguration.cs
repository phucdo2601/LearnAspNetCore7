using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnNet7AuthenAndAuthorB01.Models.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);

            //khoa phuc accountId cua bang RefreshToken lien ket voi kc o bang Account
            builder.HasOne(x => x.Account).WithMany(x => x.RefreshTokens).HasForeignKey(x => x.AccountId);
        }
    }
}
