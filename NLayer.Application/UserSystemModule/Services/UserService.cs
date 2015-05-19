using System;
using System.Linq;
using NLayer.Application.Exceptions;
using NLayer.Application.Resources;
using NLayer.Application.UserSystemModule.Converters;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.UserAgg;
using NLayer.Infrastructure.Entity;
using NLayer.Infrastructure.Helper;
using PagedList;

namespace NLayer.Application.UserSystemModule.Services
{
    public class UserService : IUserService
    {
        IUserRepository _Repository;

        #region Constructors

        public UserService(IUserRepository repository)                               
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            _Repository = repository;
        }

        #endregion

        public UserDTO Add(UserDTO userDTO)
        {
            var user = userDTO.ToModel();
            user.Id = IdentityGenerator.NewSequentialGuid();
            user.Created = DateTime.UtcNow;

            if (user.Name.IsNullOrBlank())
            {
                throw new DataExistsException(UserSystemResource.Common_Name_Empty);
            }

            if (_Repository.Exists(user))
            {
                throw new DataExistsException(UserSystemResource.User_Exists);
            }

            user.LoginPwd = AuthService.EncryptPassword(user.LoginPwd);
            _Repository.Add(user);

            //commit the unit of work
            _Repository.UnitOfWork.Commit();

            return user.ToDto();
        }

        public void Update(UserDTO userDTO)
        {
            //get persisted item
            var persisted = _Repository.Get(userDTO.Id);

            if (persisted != null) //if customer exist
            {
                var current = userDTO.ToModel();
                current.Created = persisted.Created;    //不修改创建时间
                current.LoginPwd = persisted.LoginPwd;    //不修改密码

                if (current.Name.IsNullOrBlank())
                {
                    throw new DataExistsException(UserSystemResource.Common_Name_Empty);
                }

                if (_Repository.Exists(current))
                {
                    throw new DataExistsException(UserSystemResource.User_Exists);
                }

                //Merge changes
                _Repository.Merge(persisted, current);

                //commit unit of work
                _Repository.UnitOfWork.Commit();
            }
            else
            {
                throw new DataNotFoundException(UserSystemResource.User_NotExists);
            }
        }

        public void Remove(Guid id)
        {
            var user = _Repository.Get(id);

            if (user != null) //if exist
            {
                _Repository.Remove(user);

                //commit unit of work
                _Repository.UnitOfWork.Commit();
            }
            else
            {
                // Not Exists
            }
        }
        public UserDTO FindBy(Guid id)
        {
            return _Repository.Get(id).ToDto();
        }


        public IPagedList<UserDTO> FindBy(string name, int pageNumber, int pageSize)
        {
            var list = _Repository.FindBy(name, pageNumber, pageSize);
            return new StaticPagedList<UserDTO>(
               list.ToList().Select(x => x.ToDto()),
               pageNumber,
               pageSize,
               list.TotalItemCount);
        }
    }
}
