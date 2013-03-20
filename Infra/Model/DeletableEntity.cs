using System;

namespace Infra.Model
{
    public abstract class DeletableEntity : Entity, IDeletableEntity
    {
        public Boolean IsDeleted { get; set; }
    }
}
