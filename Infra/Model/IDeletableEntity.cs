using System;

namespace Infra.Model
{
    public interface IDeletableEntity : IEntity
    {
        Boolean IsDeleted { get; set; }
    }
}
