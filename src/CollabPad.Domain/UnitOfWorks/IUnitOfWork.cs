using System;
using System.Linq;

namespace CollabPad.Domain.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
