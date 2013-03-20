using System;
using Infra.Model;

namespace Infra.Interfaces.DAL
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUserName(String userName);
    }
}
