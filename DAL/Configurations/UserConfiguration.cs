using Infra.Model;

namespace DAL.Configurations
{
    internal class UserConfiguration : DeletableEntityConfiguration<User>
    {
        internal UserConfiguration()
        {
            this.ToTable("Users");

            this.Property(x => x.UserName).HasMaxLength(50).IsRequired();
            this.Property(x => x.EmailAddress).HasMaxLength(255).IsRequired();
            this.Property(x => x.DisplayName).HasMaxLength(50).IsOptional();
        }
    }
}
