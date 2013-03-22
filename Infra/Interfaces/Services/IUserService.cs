using System;
using Infra.Model;

namespace Infra.Interfaces.Services
{
    public interface IUserService
    {
        UserProfile GetByUserName(String userName);
    }
}
