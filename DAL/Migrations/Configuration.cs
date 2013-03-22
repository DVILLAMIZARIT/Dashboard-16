namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;
    using Infra.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.DataContext context)
        {
            UserProfile profile = new UserProfile()
            {
                UserName = "Admin",
                DisplayName = "Administrator",
                EmailAddress = "admin@contoso.com",
                IsDeleted = false
            };
            context.UserProfiles.Add(profile);
            context.SaveChanges();
        }
    }
}
