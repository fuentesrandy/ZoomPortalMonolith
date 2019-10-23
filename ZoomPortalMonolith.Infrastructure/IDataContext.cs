using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZoomPortalMonolith.SharedKernal.Infrastructure
{
    public interface IDataContext
    {
        int Commit();
        Task<int> CommitAsync();
        void Dispose();

    }
}
