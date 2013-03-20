using System;

namespace Infra.Model
{
    public abstract class Entity : IEntity
    {
        public Int32 Id { get; set; }

        public byte[] Version { get; set; }
    }
}
