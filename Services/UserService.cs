using System;
using Infra.Interfaces.DAL;
using Infra.Interfaces.Services;
using Infra.Model;

namespace Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserProfile GetByUserName(String userName)
        {
            return this.userRepository.GetByUserName(userName);
        }
    }
}
