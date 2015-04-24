using System;
using NLayer.Application.UserSystemModule.Converters;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.UserAgg;
using NLayer.Infrastructure.Entity;
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

                //Merge changes
                _Repository.Merge(persisted, current);

                //commit unit of work
                _Repository.UnitOfWork.Commit();
            }
            else
            {
                // Not Exists
            }
        }

        public void Remove(UserDTO userDTO)
        {
            var user = _Repository.Get(userDTO.Id);

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

        public IPagedList<UserDTO> FindBy(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
