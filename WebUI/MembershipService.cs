using System;
using System.Collections.Generic;
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

        public void AddUserToRoles(String userName, params String[] roleNames)
        {
            if (!this.UserNameExists(userName))
            {
                throw new InvalidOperationException("userName does not exist");
            }
            roleProvider.AddUsersToRoles(new[] { userName }, roleNames);
        }

        public Boolean ChangePassword(String userName, String currentPassword, String newPassword)
        {
            if (!this.UserNameExists(userName))
            {
                throw new InvalidOperationException("userName does not exist");
            }
            return WebSecurity.ChangePassword(userName, currentPassword, newPassword);
        }

        public void CreateAccount(String userName, String password, String emailAddress = null, String displayName = null)
        {
            if (this.UserNameExists(userName))
            {
                throw new InvalidOperationException("userName already exists");
            }
            String token = WebSecurity.CreateUserAndAccount(userName, password, new
            {
                DisplayName = displayName,
                EmailAddress = emailAddress,
                IsDeleted = false
            }, false);
        }

        public void CreateRoles(params String[] roleNames)
        {
            foreach (String roleName in roleNames)
            {
                if (this.RoleExists(roleName))
                {
                    throw new InvalidOperationException(String.Format("role '{0}' already exists", roleName));
                }
                this.roleProvider.CreateRole(roleName);
            }
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
            if (this.IsAuthenticated)
            {
                return this.userProfileRepository.One(this.CurrentUserId);
            }
            return null;
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

        public IEnumerable<String> GetRoles()
        {
            if (this.IsAuthenticated)
            {
                return this.GetRolesById(this.CurrentUserId);
            }
            return new String[0];
        }

        public IEnumerable<String> GetRolesById(Int32 id)
        {
            var profile = this.GetProfileById(id);
            if (profile != null)
            {
                return this.roleProvider.GetRolesForUser(profile.UserName);
            }
            return new String[0];
        }

        public IEnumerable<String> GetRolesByUserName(String userName)
        {
            return this.roleProvider.GetRolesForUser(userName);
        }

        public Boolean IsAuthenticated
        {
            get { return WebSecurity.IsAuthenticated; }
        }

        public Boolean Login(String userName, String password, Boolean persistCookie = false)
        {
            return WebSecurity.Login(userName, password, persistCookie);
        }

        public void Logout()
        {
            WebSecurity.Logout();
        }

        public bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public Boolean UpdateProfile(UserProfile profile)
        {
            return this.userProfileRepository.Save(profile) != null;
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