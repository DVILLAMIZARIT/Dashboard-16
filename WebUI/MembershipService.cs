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

        #region IMembershipService

        public Boolean ChangePassword(String userName, String currentPassword, String newPassword)
        {
            return WebSecurity.ChangePassword(userName, currentPassword, newPassword);
        }

        public void CreateAccount(String userName, String password, String emailAddress = null, String displayName = null)
        {
            String token = WebSecurity.CreateUserAndAccount(userName, password, new
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

        public UserProfile GetProfile()
        {
            return this.userProfileRepository.One(this.CurrentUserId);
        }

        public UserProfile GetProfileByEmail(String emailAddress)
        {
            return this.userProfileRepository.One(x => x.EmailAddress == emailAddress);
        }

        public UserProfile GetProfileById(Int32 id)
        {
            return this.userProfileRepository.One(id);
        }

        public UserProfile GetProfileByUserName(String userName)
        {
            return this.userProfileRepository.GetByUserName(userName);
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

        public Boolean UserNameExists(String userName)
        {
            return this.userProfileRepository.One(x => x.UserName == userName) != null;
        }

        public Boolean UserEmailExists(String emailAddress)
        {
            return this.userProfileRepository.One(x => x.EmailAddress == emailAddress) != null;
        }

        #endregion
    }
}