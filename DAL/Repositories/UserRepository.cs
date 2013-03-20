using System;
using System.Linq;
using Infra.Interfaces.DAL;
using Infra.Model;

namespace DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository()
            : base()
        {
        }
        public UserRepository(DataContext context)
            : base(context)
        {
        }

        public User GetByUserName(String userName)
        {
            return this.All().FirstOrDefault(x => x.UserName == userName);
        }
    }
}
