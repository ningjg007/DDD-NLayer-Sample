using System;
using System.Collections.Generic;
using System.Linq;
using NLayer.Application.UserSystemModule.Converters;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg;
using NLayer.Infrastructure.Entity;

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

        public void Remove(RoleGroupDTO roleGroupDTO)
        {
            var roleGroup = _Repository.Get(roleGroupDTO.Id);

            if (roleGroup != null) //if exist
            {
                _Repository.Remove(roleGroup);

                //commit unit of work
                _Repository.UnitOfWork.Commit();
            }
            else
            {
                // Not Exists
            }
        }

        public List<RoleGroupDTO> FindAll()
        {
            return _Repository.FindAll().Select(x => x.ToDto()).ToList();
        }
    }
}
