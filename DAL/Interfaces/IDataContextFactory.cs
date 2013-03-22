using System;

namespace DAL.Interfaces
{
    public interface IDataContextFactory : IDisposable
    {
        DataContext Create();
    }
}
