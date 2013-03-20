using Infra.Model;

namespace DAL.Configurations
{
    internal abstract class DeletableEntityConfiguration<TEntity> : EntityConfiguration<TEntity>
        where TEntity : DeletableEntity
    {
        public DeletableEntityConfiguration()
        {
            this.Property(x => x.IsDeleted).IsRequired();
        }
    }
}
