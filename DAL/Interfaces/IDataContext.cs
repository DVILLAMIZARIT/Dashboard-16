using System.Data.Entity;
using Infra.Model;

namespace DAL.Interfaces
{
    public interface IDataContext
    {
        DbSet<User> Users { get; set; }
    }
}
