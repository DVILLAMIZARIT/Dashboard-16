using System;
using Infra.Model;

namespace Infra.Interfaces.DAL
{
    public interface IUserRepository : IRepository<UserProfile>
    {
        UserProfile GetByUserName(String userName);
    }
}
