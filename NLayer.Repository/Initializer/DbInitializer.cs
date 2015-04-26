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
            // 初始化时使用
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<NLayerUnitOfWork, Configuration>());

            // 运行时使用
            Database.SetInitializer<NLayerUnitOfWork>(null);
        }
    }
}