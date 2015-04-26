using System;
using System.Collections.Generic;
using System.Linq;
using NLayer.Application.Exceptions;
using NLayer.Application.Resources;
using NLayer.Application.UserSystemModule.Converters;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.RoleAgg;
using NLayer.Infrastructure.Entity;
using NLayer.Infrastructure.Helper;
using PagedList;

namespace NLayer.Application.UserSystemModule.Services
{
    public class RoleService : IRoleService
    {
        IRoleRepository _Repository;

        #region Constructors

        public RoleService(IRoleRepository repository)                               
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            _Repository = repository;
        }

        #endregion

        public RoleDTO Add(RoleDTO roleDTO)
        {
            var role = roleDTO.ToModel();
            role.Id = IdentityGenerator.NewSequentialGuid();
            role.Created = DateTime.UtcNow;

            if (role.Name.IsNullOrBlank())
            {
                throw new DataExistsException(UserSystemResource.Common_Name_Empty);
            }

            if (_Repository.Exists(role))
            {
                throw new DataExistsException(UserSystemResource.Role_Exists);
            }

            _Repository.Add(role);

            //commit the unit of work
            _Repository.UnitOfWork.Commit();

            return role.ToDto();
        }

        public void Update(RoleDTO roleDTO)
        {
            //get persisted item
            var persisted = _Repository.Get(roleDTO.Id);

            if (persisted != null) //if customer exist
            {
                var current = roleDTO.ToModel();
                current.Created = persisted.Created;    //不修改创建时间

                if (current.Name.IsNullOrBlank())
                {
                    throw new DataExistsException(UserSystemResource.Common_Name_Empty);
                }

                if (_Repository.Exists(current))
                {
                    throw new DataExistsException(UserSystemResource.Role_Exists);
                }

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

        public void Remove(Guid id)
        {
            var role = _Repository.Get(id);

            if (role != null) //if exist
            {
                _Repository.Remove(role);

                //commit unit of work
                _Repository.UnitOfWork.Commit();
            }
            else
            {
                throw new DataNotFoundException(UserSystemResource.Role_NotExists);
            }
        }

        public List<RoleDTO> FindAll()
        {
            return _Repository.FindAll().OrderBy(x => x.SortOrder)
                .Select(x => x.ToDto()).ToList();
        }

        public RoleDTO FindBy(Guid id)
        {
            return _Repository.Get(id).ToDto();
        }

        public IPagedList<RoleDTO> FindBy(Guid roleGroupId, string name, int pageNumber, int pageSize)
        {
            var list = _Repository.FindBy(roleGroupId, name, pageNumber, pageSize);
            return new StaticPagedList<RoleDTO>(
               list.ToList().Select(x => x.ToDto()),
               pageNumber,
               pageSize,
               list.TotalItemCount);
        }
    }
}
