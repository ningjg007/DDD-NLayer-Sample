using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Domain.UserSystemModule.Aggregates.MenuAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg;
using NLayer.Domain.UserSystemModule.Aggregates.UserAgg;

namespace NLayer.Repository.UnitOfWork
{
    public class NLayerUnitOfWork : DbContext, INLayerUnitOfWork
    {
        public NLayerUnitOfWork()
            : base("NLayerContext")
        {
            Initializer.DbInitializer.Initialize();
        }

        public virtual DbSet<RoleGroup> RoleGroups { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Role>().HasRequired(x => x.RoleGroup).WithMany(x => x.Roles);

            mb.Entity<Menu>().HasMany(x => x.Permissions).WithRequired(x => x.Menu);

            mb.Entity<User>().HasMany(x => x.Groups).WithMany(x => x.Users).Map(x => x.MapLeftKey("User_Id").MapRightKey("RoleGroup_Id").ToTable("RoleGroup_User", "auth"));

            mb.Entity<User>().HasMany(x => x.Permissions).WithMany(x => x.Users).Map(x => x.MapLeftKey("User_Id").MapRightKey("Permission_Id").ToTable("User_Permission", "auth"));

            mb.Entity<Role>().HasMany(x => x.Permissions).WithMany(x => x.Roles).Map(x => x.MapLeftKey("Role_Id").MapRightKey("Permission_Id").ToTable("Role_Permission", "auth"));
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            //if not is attached, attach original and set current values
            base.Entry<TEntity>(original).CurrentValues.SetValues(current);
        }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public IDbSet<TEntity> CreateSet<TEntity>()
           where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                        .ForEach(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValues()));

                }
            } while (saveFailed);
        }

        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            base.ChangeTracker.Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Unchanged);
        }
    }
}
