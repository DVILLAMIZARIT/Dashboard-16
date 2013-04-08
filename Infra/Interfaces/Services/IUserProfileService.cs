using System;
using Infra.Model;

namespace Infra.Interfaces.Services
{
    public interface IUserProfileService
    {
        UserProfile GetByUserName(String userName);
    }
}
