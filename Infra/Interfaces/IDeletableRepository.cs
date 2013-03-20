using Infra.Model;

namespace Infra.Interfaces
{
    public interface IDeletableRepository<T> : IRepository<T>
        where T : class, IDeletableEntity
    {
        T Restore(T entity);
    }
}
