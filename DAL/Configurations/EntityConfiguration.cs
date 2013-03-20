using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Infra.Model;

namespace DAL.Configurations
{
    internal abstract class EntityConfiguration<TEntity> : EntityTypeConfiguration<TEntity>
        where TEntity : Entity
    {
        internal EntityConfiguration()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.Version).IsConcurrencyToken().IsRowVersion();
        }
    }
}
