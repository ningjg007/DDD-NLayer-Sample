using System;
using System.Collections.Generic;
using System.Linq;
using NLayer.Application.Exceptions;
using NLayer.Application.Resources;
using NLayer.Application.UserSystemModule.Converters;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.MenuAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg;
using NLayer.Infrastructure.Entity;
using NLayer.Infrastructure.Utility.Helper;
using PagedList;

namespace NLayer.Application.UserSystemModule.Services
{
    public class RoleService : IRoleService
    {
        IRoleRepository _Repository;
        IRoleGroupRepository _RoleGroupRepository;
        IPermissionRepository _PermissionRepository;

        #region Constructors

        public RoleService(IRoleRepository repository, IRoleGroupRepository _roleGroupRepository, IPermissionRepository permissionRepository)                               
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            _Repository = repository;
            _RoleGroupRepository = _roleGroupRepository;
            _PermissionRepository = permissionRepository;
        }

        #endregion

        public RoleDTO Add(RoleDTO roleDTO)
        {
            var role = roleDTO.ToModel();
            role.Id = IdentityGenerator.NewSequentialGuid();
            role.Created = DateTime.UtcNow;

            var group = _RoleGroupRepository.Get(roleDTO.RoleGroupId);
            if (group == null)
            {
                throw new DataExistsException(UserSystemResource.RoleGroup_NotExists);
            }
            role.RoleGroup = group;

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

        public void UpdateRolePermission(Guid id, List<Guid> permissions)
        {
            //get persisted item
            var persisted = _Repository.Get(id);

            if (persisted != null) //if customer exist
            {
                var pList = new List<Permission>();
                foreach (var pid in permissions)
                {
                    var p = _PermissionRepository.Get(pid);
                    if (p != null)
                    {
                        pList.Add(p);
                    }
                }

                // 删除旧的权限
                persisted.Permissions.Clear();
                // 添加新的权限
                persisted.Permissions = pList;

                //commit unit of work
                _Repository.UnitOfWork.Commit();
            }
        }

        public List<PermissionDTO> GetRolePermission(Guid id)
        {
            //get persisted item
            var persisted = _Repository.Get(id);

            if (persisted != null) //if customer exist
            {
                return persisted.Permissions.Select(x => x.ToDto()).ToList();
            }

            return new List<PermissionDTO>();
        }
    }
}
