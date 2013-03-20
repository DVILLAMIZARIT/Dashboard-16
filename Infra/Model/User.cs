using System;

namespace Infra.Model
{
    public class User : DeletableEntity
    {
        public String DisplayName { get; set; }

        public String EmailAddress { get; set; }

        public String UserName { get; set; }
    }
}
