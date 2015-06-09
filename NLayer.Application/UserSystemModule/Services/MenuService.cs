using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NLayer.Application.Exceptions;
using NLayer.Application.Resources;
using NLayer.Application.UserSystemModule.Converters;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.MenuAgg;
using NLayer.Infrastructure.Entity;
using NLayer.Infrastructure.Utility.Helper;
using PagedList;

namespace NLayer.Application.UserSystemModule.Services
{
    public class MenuService : IMenuService
    {
        IMenuRepository _Repository;
        IPermissionRepository _PermissionRepository;

        #region Constructors

        public MenuService(IMenuRepository repository, IPermissionRepository permissionRepository)                               
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            _Repository = repository;
            _PermissionRepository = permissionRepository;
        }

        #endregion

        public MenuDTO Add(MenuDTO menuDTO)
        {
            var menu = menuDTO.ToModel();
            menu.Id = IdentityGenerator.NewSequentialGuid();
            menu.Created = DateTime.UtcNow;

            if (menu.Name.IsNullOrBlank())
            {
                throw new DataExistsException(UserSystemResource.Common_Name_Empty);
            }

            if (menu.Module.IsNullOrBlank())
            {
                throw new DataExistsException(UserSystemResource.Menu_ModuleEmpty);
            }

            if (_Repository.Exists(menu))
            {
                throw new DataExistsException(UserSystemResource.Menu_Exists);
            }

            foreach (var p in menu.Permissions)
            {
                if (p.Id == Guid.Empty)
                {
                    p.Id = IdentityGenerator.NewSequentialGuid();
                    p.Created = DateTime.UtcNow;
                }
            }

            _Repository.Add(menu);

            //commit the unit of work
            _Repository.UnitOfWork.Commit();

            return menu.ToDto();
        }

        public void Update(MenuDTO menuDTO)
        {
            //get persisted item
            var persisted = _Repository.Get(menuDTO.Id);

            if (persisted != null) //if customer exist
            {
                var current = menuDTO.ToModel();
                current.Created = persisted.Created;    //不修改创建时间

                if (current.Name.IsNullOrBlank())
                {
                    throw new DataExistsException(UserSystemResource.Common_Name_Empty);
                }

                if (current.Module.IsNullOrBlank())
                {
                    throw new DataExistsException(UserSystemResource.Menu_ModuleEmpty);
                }

                if (_Repository.Exists(current))
                {
                    throw new DataExistsException(UserSystemResource.Menu_Exists);
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
            var menu = _Repository.Get(id);

            if (menu != null) //if exist
            {
                _Repository.Remove(menu);

                //commit unit of work
                _Repository.UnitOfWork.Commit();
            }
            else
            {
                throw new DataNotFoundException(UserSystemResource.Menu_NotExists);
            }
        }

        public void UpdatePermission(MenuDTO menuDTO)
        {
            //get persisted item
            var persisted = _Repository.Get(menuDTO.Id);

            if (persisted != null) //if customer exist
            {

                foreach (var p in menuDTO.Permissions)
                {
                    if (p.Id == Guid.Empty)
                    {
                        p.Id = IdentityGenerator.NewSequentialGuid();
                        p.Created = DateTime.UtcNow;
                    }
                }

                foreach (var p in persisted.Permissions.ToArray())
                {
                    // 先删除从表数据
                    _PermissionRepository.Remove(p);
                }
                persisted.Permissions = new Collection<Permission>();
                foreach (var p in menuDTO.Permissions.Select(x => x.ToModel()))
                {
                    p.Menu = persisted;
                    persisted.Permissions.Add(p);
                }

                //commit unit of work
                _Repository.UnitOfWork.Commit();
            }
        }

        public MenuDTO FindBy(Guid id)
        {
            return _Repository.Get(id).ToDto();
        }
        public IPagedList<MenuDTO> FindBy(string module, string name, int pageNumber, int pageSize)
        {
            var list = _Repository.FindBy(module, name, pageNumber, pageSize);
            return new StaticPagedList<MenuDTO>(
               list.ToList().Select(x=>x.ToDto()),
               pageNumber,
               pageSize,
               list.TotalItemCount);
        }
    }
}
