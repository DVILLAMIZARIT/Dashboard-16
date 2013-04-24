using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Infra.Model;

namespace Infra.Interfaces.Services
{
    [ContractClass(typeof(MembershipServiceContract))]
    public interface IMembershipService
    {
        Boolean ChangePassword(String userName, String currentPassword, String newPassword);

        void CreateAccount(String userName, String password, String emailAddress = null, String displayName = null);

        Int32 CurrentUserId { get; }

        String CurrentUserName { get; }

        UserProfile GetProfile();

        UserProfile GetProfileByEmail(String emailAddress);

        UserProfile GetProfileById(Int32 id);

        UserProfile GetProfileByUserName(String userName);

        IEnumerable<String> GetRoles();

        IEnumerable<String> GetRolesById(Int32 id);

        IEnumerable<String> GetRolesByUserName(String userName);

        Boolean IsAuthenticated { get; }

        Boolean Login(String userName, String password, Boolean persistCookie = false);
        
        void Logout();

        Boolean UpdateProfile(UserProfile profile);
        
        Boolean UserNameExists(String userName);

        Boolean UserEmailExists(String emailAddress);
    }

    [ContractClassFor(typeof(IMembershipService))]
    internal abstract class MembershipServiceContract : IMembershipService
    {
        public Boolean ChangePassword(String userName, String currentPassword, String newpassword)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(userName));
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(currentPassword));
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(newpassword));

            throw new NotImplementedException();
        }

        public void CreateAccount(String username, String password, String emailAddress = null, String displayName = null)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(username));
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(password));

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
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(emailAddress));

            throw new NotImplementedException();
        }

        public UserProfile GetProfileById(Int32 id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);

            throw new NotImplementedException();
        }

        public UserProfile GetProfileByUserName(String username)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(username));

            throw new NotImplementedException();
        }

        public IEnumerable<String> GetRoles()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<String> GetRolesById(Int32 id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);

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
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(userName));
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(password));

            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public Boolean UpdateProfile(UserProfile profile)
        {
            Contract.Requires<ArgumentNullException>(profile != null);

            throw new NotImplementedException();
        }

        public Boolean UserNameExists(String userName)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(userName));

            throw new NotImplementedException();
        }

        public Boolean UserEmailExists(String emailAddress)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(emailAddress));

            throw new ArgumentNullException();
        }
    }
}
