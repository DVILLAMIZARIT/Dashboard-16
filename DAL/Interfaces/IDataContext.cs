using System;
using System.Data.Entity;
using Infra.Model;

namespace DAL.Interfaces
{
    public interface IDataContext : IDisposable
    {
        DbSet<UserProfile> UserProfiles { get; set; }
    }
}
