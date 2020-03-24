using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.Utils.ORM.Imp
{
    public interface IUnitOfWork<TContext> : IDisposable
        where TContext : DbContext
    {
        void BeginTran();
        void CommitTran();
        void RollBackTran();
    }
}
