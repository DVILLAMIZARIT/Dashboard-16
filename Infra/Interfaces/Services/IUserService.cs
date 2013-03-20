using System;
using Infra.Model;

namespace Infra.Interfaces.Services
{
    public interface IUserService
    {
        User GetByUserName(String userName);
    }
}
