using System;
using System.Collections.Generic;
using System.Linq;
using NLayer.Application.Exceptions;
using NLayer.Application.Resources;
using NLayer.Application.UserSystemModule.Converters;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg;
using NLayer.Infrastructure.Entity;
using NLayer.Infrastructure.Utility.Helper;
using PagedList;

namespace NLayer.Application.UserSystemModule.Services
{
    public class RoleGroupService : IRoleGroupService
    {
        IRoleGroupRepository _Repository;

        #region Constructors

        public RoleGroupService(IRoleGroupRepository repository)                               
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            _Repository = repository;
        }

        #endregion

        public RoleGroupDTO Add(RoleGroupDTO roleGroupDTO)
        {
            var roleGroup = roleGroupDTO.ToModel();
            roleGroup.Id = IdentityGenerator.NewSequentialGuid();
            roleGroup.Created = DateTime.UtcNow;

            if (roleGroup.Name.IsNullOrBlank())
            {
                throw new DataExistsException(UserSystemResource.Common_Name_Empty);
            }

            if (_Repository.Exists(roleGroup))
            {
                throw new DataExistsException(UserSystemResource.RoleGroup_Exists);
            }

            _Repository.Add(roleGroup);

            //commit the unit of work
            _Repository.UnitOfWork.Commit();

            return roleGroup.ToDto();
        }

        public void Update(RoleGroupDTO roleGroupDTO)
        {
            //get persisted item
            var persisted = _Repository.Get(roleGroupDTO.Id);

            if (persisted != null) //if customer exist
            {
                var current = roleGroupDTO.ToModel();
                current.Created = persisted.Created;    //不修改创建时间

                if (current.Name.IsNullOrBlank())
                {
                    throw new DataExistsException(UserSystemResource.Common_Name_Empty);
                }

                if (_Repository.Exists(current))
                {
                    throw new DataExistsException(UserSystemResource.RoleGroup_Exists);
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
            var roleGroup = _Repository.Get(id);

            if (roleGroup != null) //if exist
            {
                _Repository.Remove(roleGroup);

                //commit unit of work
                _Repository.UnitOfWork.Commit();
            }
            else
            {
                throw new DataNotFoundException(UserSystemResource.RoleGroup_NotExists);
            }
        }

        public List<RoleGroupDTO> FindAll()
        {
            return _Repository.FindAll().OrderBy(x => x.SortOrder)
                .Select(x => x.ToDto()).ToList();
        }

        public RoleGroupDTO FindBy(Guid id)
        {
            return _Repository.Get(id).ToDto();
        }

        public IPagedList<RoleGroupDTO> FindBy(string name, int pageNumber, int pageSize)
        {
            var list = _Repository.FindBy(name, pageNumber, pageSize);
            return new StaticPagedList<RoleGroupDTO>(
               list.ToList().Select(x => x.ToDto()),
               pageNumber,
               pageSize,
               list.TotalItemCount);
        }
    }
}
