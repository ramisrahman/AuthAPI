using Authorization.Common.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Common.Database.Persistence.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users", AppDbContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
        }
    }
}
