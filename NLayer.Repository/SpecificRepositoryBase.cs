using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NLayer.Infrastructure.Entity;
using NLayer.Infrastructure.Repository;
using NLayer.Repository.UnitOfWork;

namespace NLayer.Repository
{
    public abstract class SpecificRepositoryBase<TEntity> : RepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        protected SpecificRepositoryBase(INLayerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        private IDbSet<TEntity> _Table;

        protected IDbSet<TEntity> Table
        {
            get
            {
                if (_Table == null)
                {
                    _Table = (UnitOfWork as INLayerUnitOfWork).CreateSet<TEntity>();
                }
                return _Table;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override void SaveChanges()
        {
            ((INLayerUnitOfWork) UnitOfWork).DbContext.SaveChanges();
        }

        public virtual void Dispose(bool isDisposing)
        {
            if (!isDisposing)
            {
                return;
            }

            if (UnitOfWork != null)
            {
                UnitOfWork.Dispose();
            }
        }

        public override TEntity Get(object key)
        {
            return Table.Find(key);
        }

        public override void Merge(TEntity persisted, TEntity current)
        {
            ((INLayerUnitOfWork)UnitOfWork).ApplyCurrentValues(persisted, current);
        }

        public override IEnumerable<TEntity> FindAll()
        {
            return Table.ToList();
        }

        public override void Add(TEntity item)
        {
            if (item == null) throw new ArgumentNullException("item");
            Table.Add(item);
        }

        public override void Remove(TEntity item)
        {
            if (item == null) throw new ArgumentNullException("item");
            Table.Remove(item);
        }

        public override bool Exists(TEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
