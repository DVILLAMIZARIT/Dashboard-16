using System;
using System.Linq;
using DAL.Interfaces;
using Infra.Interfaces.DAL;
using Infra.Model;

namespace DAL.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(IDataContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        public UserProfile GetByUserName(String userName)
        {
            return this.All().FirstOrDefault(x => x.UserName == userName);
        }
    }
}
