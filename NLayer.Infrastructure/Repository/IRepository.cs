using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Infrastructure.Entity;
using NLayer.Infrastructure.UnitOfWork;

namespace NLayer.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }
        TEntity Get(object key);
        void Merge(TEntity persisted, TEntity current);
        IEnumerable<TEntity> FindAll();
        void Add(TEntity item);
        void Remove(TEntity item);
        bool Exists(TEntity item);
        TEntity Find(Func<TEntity, bool> acquire);
        IQueryable<TEntity> Collection { get; }
    }
}
