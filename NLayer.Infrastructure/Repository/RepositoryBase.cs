using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using NLayer.Infrastructure.Entity;
using NLayer.Infrastructure.UnitOfWork;

namespace NLayer.Infrastructure.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase
    {
        #region Members

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        protected RepositoryBase(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == (IUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork");

            UnitOfWork = unitOfWork;
        }

        #endregion

        protected void SaveChangesIfNotInTransaction()
        {
            if (!IsInTransaction())
            {
                SaveChanges();
            }
        }

        /// <summary>
        /// Detect if is in transaction or not.
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsInTransaction()
        {
            return Transaction.Current != null;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public abstract void SaveChanges();

        public abstract TEntity Get(object key);
        public abstract void Merge(TEntity persisted, TEntity current);
        public abstract IEnumerable<TEntity> FindAll();
        public abstract void Add(TEntity item);
        public abstract void Remove(TEntity item);
    }
}
