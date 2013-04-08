using System;
using Infra.Interfaces.DAL;
using Infra.Interfaces.Services;
using Infra.Model;

namespace Services
{
    public class UserProfileService : IUserProfileService
    {
        private IUserProfileRepository userRepository;

        public UserProfileService(IUserProfileRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserProfile GetByUserName(String userName)
        {
            return this.userRepository.GetByUserName(userName);
        }
    }
}
