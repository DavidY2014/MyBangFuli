using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.Utils.ORM.Imp
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
       where TDbContext : DbContext
    {
        private IDbContextManager<TDbContext> _dbContextManager;
        private IDbContextTransaction _dbContextTransaction;
        private bool disposed = false;

        public UnitOfWork(IDbContextManager<TDbContext> dbContextManager)
        {
            if (dbContextManager == null)
            {
                throw new ArgumentNullException("dbContextManager");
            }
            this._dbContextManager = dbContextManager;
        }

        public void BeginTran()
        {
            if (this._dbContextManager.Master != null)
            {
                _dbContextTransaction = this._dbContextManager.Master.Database.BeginTransaction();
            }
        }

        public void CommitTran()
        {
            if (_dbContextTransaction != null)
            {
                _dbContextTransaction.Commit();
            }
        }

        public virtual void Dispose(bool isDispose)
        {
            if (isDispose)
            {
                if (!disposed)
                {
                    _dbContextManager.Dispose();
                    disposed = true;
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void RollBackTran()
        {
            if (_dbContextTransaction != null)
            {
                _dbContextTransaction.Rollback();
            }
        }
    }
}
