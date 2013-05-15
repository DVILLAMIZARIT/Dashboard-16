using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Infra.Model;

namespace Infra.Interfaces.Services
{
    [ContractClass(typeof(MembershipServiceContract))]
    public interface IMembershipService
    {
        void AddUserToRoles(String userName, params String[] roleNames);

        Boolean ChangePassword(String userName, String currentPassword, String newPassword);

        void CreateAccount(String userName, String password, String emailAddress = null, String displayName = null);

        void CreateRoles(params String[] roleNames);

        Int32 CurrentUserId { get; }

        String CurrentUserName { get; }

        UserProfile GetProfile();

        UserProfile GetProfileByEmail(String emailAddress);

        UserProfile GetProfileById(Int32 userId);

        UserProfile GetProfileByUserName(String userName);

        IEnumerable<String> GetRoles();

        IEnumerable<String> GetRolesById(Int32 userId);

        IEnumerable<String> GetRolesByUserName(String userName);

        Boolean IsAuthenticated { get; }

        Boolean Login(String userName, String password, Boolean persistCookie = false);
        
        void Logout();

        Boolean RoleExists(String roleName);

        Boolean UpdateProfile(UserProfile profile);
        
        Boolean UserNameExists(String userName);

        Boolean UserEmailExists(String emailAddress);
    }

    [ContractClassFor(typeof(IMembershipService))]
    internal abstract class MembershipServiceContract : IMembershipService
    {
        public void AddUserToRoles(String userName, params String[] roleNames)
        {
            MembershipServiceContracts.ValidateUserName(userName);
            Contract.Requires<ArgumentNullException>(roleNames != null, "roleNames cannot be null");
            Contract.Requires<ArgumentNullException>(roleNames.Length > 0, "roleNames cannot be empty");
            Contract.Requires<ArgumentException>(!roleNames.Any(x => String.IsNullOrWhiteSpace(x)), "roleNames cannot contain empty values");

            throw new NotImplementedException();
        }

        public Boolean ChangePassword(String userName, String currentPassword, String newPassword)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(userName), "userName cannot be null or empty");
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(currentPassword), "currentPassword cannot be null or empty");
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(newPassword), "newPassword cannot be null or empty");

            throw new NotImplementedException();
        }

        public void CreateAccount(String userName, String password, String emailAddress = null, String displayName = null)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(userName), "userName cannot be null or empty");
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(password));

            throw new NotImplementedException();
        }

        public void CreateRoles(params String[] roleNames)
        {
            Contract.Requires<ArgumentNullException>(roleNames != null, "roleNames cannot be null");
            Contract.Requires<ArgumentNullException>(roleNames.Length > 0, "roleNames cannot be empty");
            Contract.Requires<ArgumentException>(!roleNames.Any(x => String.IsNullOrWhiteSpace(x)), "roleNames cannot contain empty values");

            throw new NotImplementedException();
        }

        public Int32 CurrentUserId
        {
            get { throw new NotImplementedException(); }
        }

        public String CurrentUserName
        {
            get { throw new NotImplementedException(); }
        }

        public UserProfile GetProfile()
        {
            throw new NotImplementedException();
        }

        public UserProfile GetProfileByEmail(String emailAddress)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(emailAddress), "emailAddress cannot be null or empty");

            throw new NotImplementedException();
        }

        public UserProfile GetProfileById(Int32 userId)
        {
            Contract.Requires<ArgumentException>(userId > 0, "a valid userId must be supplied");

            throw new NotImplementedException();
        }

        public UserProfile GetProfileByUserName(String userName)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(userName), "userName cannot be null or empty");

            throw new NotImplementedException();
        }

        public IEnumerable<String> GetRoles()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<String> GetRolesById(Int32 userId)
        {
            Contract.Requires<ArgumentException>(userId > 0, "a valid userId must be supplied");

            throw new NotImplementedException();
        }

        public IEnumerable<String> GetRolesByUserName(String userName)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(userName));

            throw new NotImplementedException();
        }

        public Boolean IsAuthenticated
        {
            get { throw new NotImplementedException(); }
        }

        public Boolean Login(String userName, String password, Boolean persistCookie = false)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(userName), "userName cannot be null or empty");
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(password));

            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public Boolean RoleExists(String roleName)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(roleName));

            throw new ArgumentNullException();
        }

        public Boolean UpdateProfile(UserProfile profile)
        {
            Contract.Requires<ArgumentNullException>(profile != null);

            throw new NotImplementedException();
        }

        public Boolean UserNameExists(String userName)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(userName), "userName cannot be null or empty");

            throw new NotImplementedException();
        }

        public Boolean UserEmailExists(String emailAddress)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(emailAddress), "emailAddress cannot be null or empty");

            throw new ArgumentNullException();
        }
    }

    //TODO Refactor above to use this helper class
    internal static class MembershipServiceContracts
    {
        [ContractAbbreviator]
        public static void ValidateEmailAddress(String emailAddress)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(emailAddress), "emailAddress cannot be null or empty");
        }

        [ContractAbbreviator]
        public static void ValidateRoleNames(params String[] roleNames)
        {
            Contract.Requires<ArgumentNullException>(roleNames != null, "roleNames cannot be null");
            Contract.Requires<ArgumentNullException>(roleNames.Length > 0, "roleNames cannot be empty");
            Contract.Requires<ArgumentException>(!roleNames.Any(x => String.IsNullOrWhiteSpace(x)), "roleNames cannot contain empty values");
        }

        [ContractAbbreviator]
        public static void ValidateUserId(Int32 userId)
        {
            Contract.Requires<ArgumentException>(userId > 0, "a valid userId must be supplied");
        }

        [ContractAbbreviator]
        public static void ValidateUserName(String userName)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(userName), "userName cannot be null or empty");
        }

        [ContractAbbreviator]
        public static void ValidatePassword(String password)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(password), "password cannot be null or empty");
            Contract.Requires<ArgumentException>(password.Length > 6, "password must be at least 6 characters long");
        }
    }
}
