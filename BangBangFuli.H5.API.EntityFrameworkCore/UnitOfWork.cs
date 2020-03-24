using BangBangFuli.H5.API.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BangBangFuli.H5.API.EntityFrameworkCore
{
    public class UnitOfWork<TDBContext> : IUnitOfWork where TDBContext : DbContext
    {
        private readonly TDBContext _dbContext;

        public UnitOfWork(TDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
