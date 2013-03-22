using System;

namespace Infra.Model
{
    public class UserProfile : DeletableEntity
    {
        public String DisplayName { get; set; }

        public String EmailAddress { get; set; }

        public String UserName { get; set; }
    }
}
