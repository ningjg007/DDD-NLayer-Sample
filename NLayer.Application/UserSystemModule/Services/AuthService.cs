using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Application.Exceptions;
using NLayer.Application.Resources;
using NLayer.Application.UserSystemModule.Converters;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.MenuAgg;
using NLayer.Domain.UserSystemModule.Aggregates.UserAgg;
using NLayer.Infrastructure.Helper;
using NLayer.Infrastructure.Utility;

namespace NLayer.Application.UserSystemModule.Services
{
    public class AuthService : IAuthService
    {
        #region 私有成员

        IUserRepository _UserRepository;

        private static string AuthSecret
        {
            get
            {
                var authSecret = ConfigurationManager.AppSettings["NLayer:AuthSecret"];
                if (authSecret.IsNullOrBlank())
                {
                    throw new ConfigurationErrorsException("missing \"NLayer:AuthSecret\" appSetting.");
                }
                return authSecret;
            }
        }

        private void UpdateLoginToken(Guid id, string loginToken)
        {
            var user = _UserRepository.Get(id);

            if (user == null)
            {
                throw new ArgumentException(id.ToString(), "id");
            }

            user.LastLoginToken = loginToken;

            //commit unit of work
            _UserRepository.UnitOfWork.Commit();
        }

        #endregion

        #region Constructors

        public AuthService(IUserRepository userRepository)                               
        {
            if (userRepository == null)
                throw new ArgumentNullException("userRepository");

            _UserRepository = userRepository;
        }

        #endregion

        public UserDTO Login(string loginName, string password, bool updateLoginToken)
        {
            var user = _UserRepository.Find(x => x.LoginName.Equals(loginName));

            if (user == null)
            {
                throw new LoginNameNotFoundException(UserSystemResource.Login_NameNotFound);
            }

            if (!EncryptPassword(password).EqualsIgnoreCase(user.LoginPwd))
            {
                throw new LoginPasswordIncorrectException(UserSystemResource.Login_PasswordIncorrect);
            }

            if (updateLoginToken)
            {
                var newToken = SecurityHelper.NetxtString(24).ToLower();
                UpdateLoginToken(user.Id, newToken);
                user.LastLoginToken = newToken;
            }

            if (user.LastLoginToken.IsNullOrBlank())
            {
                throw new Exception(UserSystemResource.Login_TokenEmpty);
            }

            return user.ToDto();
        }

        public bool ValidatePassword(Guid id, string password)
        {
            var user = _UserRepository.Get(id);

            if (user == null)
            {
                throw new ArgumentException(id.ToString(), "id");
            }

            return EncryptPassword(password).EqualsIgnoreCase(user.LoginPwd);
        }

        public void ChangePassword(Guid id, string password)
        {
            var user = _UserRepository.Get(id);

            if (user == null)
            {
                throw new ArgumentException(id.ToString(), "id");
            }

            user.LoginPwd = EncryptPassword(password);

            //commit unit of work
            _UserRepository.UnitOfWork.Commit();
        }

        public bool ValidateLoginToken(Guid id, string token)
        {
            if (token.IsNullOrBlank())
            {
                throw new ArgumentException(token, "token");
            }

            var user = _UserRepository.Get(id);

            if (user == null)
            {
                throw new ArgumentException(id.ToString(), "id");
            }

            return token.EqualsIgnoreCase(user.LastLoginToken);
        }

        public UserDTO FindByLoginToken(Guid id, string token)
        {
            var user = _UserRepository.Get(id);

            if (user == null)
            {
                throw new ArgumentException(id.ToString(), "id");
            }

            if (!user.LastLoginToken.EqualsIgnoreCase(token))
            {
                throw new DataNotFoundException(UserSystemResource.User_NotExists);
            }

            return user.ToDto();
        }

        public List<PermissionForAuthDTO> GetRolePermissions(Guid id)
        {
            var user = _UserRepository.Get(id);

            if (user == null)
            {
                throw new ArgumentException(id.ToString(), "id");
            }

            var permissions = new List<PermissionForAuthDTO>();

            foreach (var role in user.Roles.Where(role => role.Permissions != null))
            {
                permissions.AddRange(role.Permissions.Select(x=>new PermissionForAuthDTO()
                {
                    PermissionId = x.Id,
                    PermissionCode = x.Code,
                    MenuId = x.Menu.Id,
                    RoleId = role.Id
                }));
            }

            return permissions;
        }

        public List<PermissionForAuthDTO> GetUserPermissions(Guid id)
        {
            var user = _UserRepository.Get(id);

            if (user == null)
            {
                throw new ArgumentException(id.ToString(), "id");
            }

            return user.Permissions != null ? user.Permissions.Select(x => new PermissionForAuthDTO()
            {
                PermissionId = x.Id,
                PermissionCode = x.Code,
                MenuId = x.Menu.Id,
                FromUser = true
            }).ToList()
                : new List<PermissionForAuthDTO>();
        }

        public static string EncryptPassword(string password)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password + "_" + AuthSecret);
            byte[] sha1Bytes = System.Security.Cryptography.SHA1.Create().ComputeHash(bytes);
            string secretPart = BitConverter.ToString(sha1Bytes).Replace("-", string.Empty);
            return secretPart;
        }
    }
}
