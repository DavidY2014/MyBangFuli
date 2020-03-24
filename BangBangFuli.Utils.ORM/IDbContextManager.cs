using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.Utils.ORM.Imp
{
    public interface IDbContextManager<TDbContext> : IDisposable
            where TDbContext : DbContext
    {
        TDbContext Master { get; }
        TDbContext Slave { get; }
    }
}
