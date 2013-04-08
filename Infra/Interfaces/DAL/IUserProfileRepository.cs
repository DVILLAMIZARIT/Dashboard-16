using System;
using Infra.Model;

namespace Infra.Interfaces.DAL
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        UserProfile GetByUserName(String userName);
    }
}
