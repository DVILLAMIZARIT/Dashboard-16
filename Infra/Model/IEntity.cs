using System;

namespace Infra.Model
{
    public interface IEntity
    {
        Int32 Id { get; set; }

        Byte[] Version { get; set; }
    }
}
