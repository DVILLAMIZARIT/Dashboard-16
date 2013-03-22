using Infra.Model;

namespace DAL.Configurations
{
    internal class UserProfileConfiguration : DeletableEntityConfiguration<UserProfile>
    {
        internal UserProfileConfiguration()
        {
            this.ToTable("UserProfiles");

            this.Property(x => x.UserName).HasMaxLength(50).IsRequired();
            this.Property(x => x.EmailAddress).HasMaxLength(255).IsRequired();
            this.Property(x => x.DisplayName).HasMaxLength(50).IsOptional();
        }
    }
}
