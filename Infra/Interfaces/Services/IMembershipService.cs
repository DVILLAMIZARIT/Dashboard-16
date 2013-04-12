using System;
using Infra.Model;

namespace Infra.Interfaces.Services
{
    public interface IMembershipService
    {
        void CreateAccount(String username, String password, String emailAddress = null, String displayName = null);

        Int32 CurrentUserId { get; }

        String CurrentUserName { get; }

        UserProfile GetProfileById(Int32? id = null);

        UserProfile GetProfileByUserName(String username = null);

        Boolean IsAuthenticated { get; }

        void Login(String userName, String password, Boolean persistCookie = false);
        
        void Logout();
        
        Boolean UserExists(String userName);
    }
}
