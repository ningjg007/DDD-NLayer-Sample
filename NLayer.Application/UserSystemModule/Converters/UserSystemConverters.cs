namespace NLayer.Application.UserSystemModule.Converters
{
    public static partial class UserSystemConverters
    {
        static UserSystemConverters()
        {
            InitRoleMappers();
            InitUserMappers();
            InitRoleGroupMappers();
            InitMenuMappers();
        }
    }
}
