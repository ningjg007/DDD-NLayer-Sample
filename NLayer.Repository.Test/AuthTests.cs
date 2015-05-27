using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Domain.UserSystemModule.Aggregates.RoleAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg;
using NLayer.Domain.UserSystemModule.Aggregates.UserAgg;
using NLayer.Infrastructure.Entity;
using NLayer.Infrastructure.Utility;
using NLayer.Infrastructure.Utility.Helper;
using NLayer.Repository.UnitOfWork;
using Xunit;

namespace NLayer.Repository.Test
{
    public class AuthTests
    {
        [Fact]
        public void AddAdmin()
        {
            var sw = new Stopwatch();
            TimeSpan timeCost;

            using (var unitOfWork = new NLayerUnitOfWork())
            {
                sw.Start();

                #region AddOrUpdate

                const string loginName = "admin";
                const string password = "123456";

                var user = unitOfWork.Users.FirstOrDefault(x => x.LoginName.Equals(loginName));

                if (user == null)
                {
                    user = new User()
                    {
                        Id = IdentityGenerator.NewSequentialGuid(),
                        Name = "管理员",
                        LoginName = loginName,
                        LoginPwd = SecurityHelper.EncryptPassword(password),
                        Created = DateTime.UtcNow,
                        LastLogin = Const.SqlServerNullDateTime
                    };

                    unitOfWork.Users.Add(user);
                }
                else
                {
                    user.Name = "管理员";
                    user.LoginPwd = SecurityHelper.EncryptPassword(password);
                    user.Created = DateTime.UtcNow;
                }

                #endregion

                unitOfWork.DbContext.SaveChanges();

                sw.Stop();
                timeCost = sw.Elapsed;
            }

            Console.WriteLine("Elapsed: " + timeCost.Ticks);
        }
    }
}
