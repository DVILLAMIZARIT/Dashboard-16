using System;
using System.Diagnostics.Contracts;
using System.Web.Security;
using Infra.Interfaces.DAL;
using Infra.Interfaces.Services;
using Infra.Model;
using WebMatrix.WebData;

namespace WebUI
{
    public class MembershipService : IMembershipService
    {
        private readonly SimpleMembershipProvider membershipProvider;
        private readonly SimpleRoleProvider roleProvider;
        private readonly IUserProfileRepository userProfileRepository;

        public MembershipService(IUserProfileRepository userProfileRepository)
        {
            Contract.Requires<ArgumentNullException>(userProfileRepository != null);

            this.membershipProvider = Membership.Provider as SimpleMembershipProvider;
            this.roleProvider = Roles.Provider as SimpleRoleProvider;
            this.userProfileRepository = userProfileRepository;
        }

        public void CreateAccount(String username, String password, String emailAddress = null, String displayName = null)
        {
            String token = WebSecurity.CreateUserAndAccount(username, password, new
            {
                DisplayName = displayName,
                EmailAddress = emailAddress,
                IsDeleted = false
            }, false);
        }

        public Int32 CurrentUserId
        {
            get { return WebSecurity.CurrentUserId; }
        }

        public String CurrentUserName
        {
            get { return WebSecurity.CurrentUserName; }
        }

        public UserProfile GetProfileById(Int32? id = null)
        {
            if (!this.IsAuthenticated)
            {
                return null;
            }
            return this.userProfileRepository.One(id ?? this.CurrentUserId);
        }

        public UserProfile GetProfileByUserName(String username = null)
        {
            if (!this.IsAuthenticated)
            {
                return null;
            }
            return this.userProfileRepository.GetByUserName(username ?? this.CurrentUserName);
        }

        public Boolean IsAuthenticated
        {
            get { return WebSecurity.IsAuthenticated; }
        }

        public void Login(String userName, String password, Boolean persistCookie = false)
        {
            WebSecurity.Login(userName, password, persistCookie);
        }

        public void Logout()
        {
            WebSecurity.Logout();
        }

        public Boolean UserExists(String userName)
        {
            return WebSecurity.UserExists(userName);
        }
    }
}