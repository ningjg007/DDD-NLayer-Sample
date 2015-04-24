using System;
using System.Collections.Generic;
using System.Linq;
using NLayer.Application.UserSystemModule.Converters;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.RoleAgg;
using NLayer.Infrastructure.Entity;

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

        public void Remove(RoleDTO roleDTO)
        {
            var role = _Repository.Get(roleDTO.Id);

            if (role != null) //if exist
            {
                _Repository.Remove(role);

                //commit unit of work
                _Repository.UnitOfWork.Commit();
            }
            else
            {
                // Not Exists
            }
        }

        public List<RoleDTO> FindAll()
        {
            return _Repository.FindAll().Select(x => x.ToDto()).ToList();
        }
    }
}
