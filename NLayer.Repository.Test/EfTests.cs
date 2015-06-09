using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using NLayer.Domain.UserSystemModule.Aggregates.MenuAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg;
using NLayer.Infrastructure.Entity;
using NLayer.Repository.UnitOfWork;
using Xunit;

namespace NLayer.Repository.Test
{
    public class EfTests
    {
        public EfTests()
        {
        }

        [Fact]
        public void QueryTest()
        {
            var sw = new Stopwatch();
            TimeSpan timeCost;
            int totalCount;

            using (var unitOfWork = new NLayerUnitOfWork())
            {
                sw.Start();

                var list = unitOfWork.Roles.ToList();

                Console.WriteLine(JsonConvert.SerializeObject(list.Select(x=>x.Name)));

                totalCount = unitOfWork.Roles.Count();

                sw.Stop();
                timeCost = sw.Elapsed;
            }

            Console.WriteLine("Total: " + totalCount);
            Console.WriteLine("Elapsed: " + timeCost.Ticks);
        }

        [Fact]
        public void InsertTest()
        {
            var sw = new Stopwatch();
            TimeSpan timeCost;

            using (var unitOfWork = new NLayerUnitOfWork())
            {
                sw.Start();

                #region Add

                var roleGroup = new RoleGroup()
                {
                    Id = IdentityGenerator.NewSequentialGuid(),
                    Name = "角色组",
                    Description = "remark",
                    Created = DateTime.UtcNow
                };

                unitOfWork.RoleGroups.Add(roleGroup);

                unitOfWork.Roles.Add(new Role()
                {
                    Id = IdentityGenerator.NewSequentialGuid(),
                    Name = "系统管理员",
                    Description = "remark",
                    RoleGroup = roleGroup,
                    Created = DateTime.UtcNow
                });

                #endregion

                unitOfWork.DbContext.SaveChanges();

                sw.Stop();
                timeCost = sw.Elapsed;
            }

            Console.WriteLine("Elapsed: " + timeCost.Ticks);
        }

        [Fact]
        public void RemoveTest()
        {
            var sw = new Stopwatch();
            TimeSpan timeCost;

            using (var unitOfWork = new NLayerUnitOfWork())
            {
                sw.Start();

                var list = unitOfWork.Roles.ToList();
                foreach (var item in list)
                {
                    unitOfWork.Roles.Remove(item);
                }

                unitOfWork.DbContext.SaveChanges();

                sw.Stop();
                timeCost = sw.Elapsed;
            }

            Console.WriteLine("Elapsed: " + timeCost.Ticks);
        }



        [Fact]
        public void InsertMenuTest()
        {
            var sw = new Stopwatch();
            TimeSpan timeCost;

            using (var unitOfWork = new NLayerUnitOfWork())
            {
                sw.Start();

                #region Add

                var menu = new Menu()
                {
                    Id = IdentityGenerator.NewSequentialGuid(),
                    Name = "菜单1",
                    Code = "menu1",
                    Created = DateTime.UtcNow
                };

                menu.Permissions = new Collection<Permission>();

                menu.Permissions.Add(new Permission()
                {
                    Id = IdentityGenerator.NewSequentialGuid(),
                    Name = "权限1",
                    Code = "Permission1",
                    Created = DateTime.UtcNow
                });

                menu.Permissions.Add(new Permission()
                {
                    Id = IdentityGenerator.NewSequentialGuid(),
                    Name = "权限2",
                    Code = "Permission2",
                    Created = DateTime.UtcNow
                });

                unitOfWork.Menus.Add(menu);

                #endregion

                unitOfWork.DbContext.SaveChanges();

                sw.Stop();
                timeCost = sw.Elapsed;
            }

            Console.WriteLine("Elapsed: " + timeCost.Ticks);
        }
    }
}
