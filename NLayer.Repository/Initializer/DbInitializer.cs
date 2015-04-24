using System.Data.Entity;
using NLayer.Repository.Migrations;
using NLayer.Repository.UnitOfWork;

namespace NLayer.Repository.Initializer
{
    public static class DbInitializer
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void Initialize()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<NLayerUnitOfWork, Configuration>());

            Database.SetInitializer<NLayerUnitOfWork>(null);
        }
    }
}