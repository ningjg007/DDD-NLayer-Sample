using System;
using System.Linq;
using NLayer.Application.Exceptions;
using NLayer.Application.Resources;
using NLayer.Application.UserSystemModule.Converters;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.MenuAgg;
using NLayer.Infrastructure.Entity;
using NLayer.Infrastructure.Helper;
using PagedList;

namespace NLayer.Application.UserSystemModule.Services
{
    public class MenuService : IMenuService
    {
        IMenuRepository _Repository;

        #region Constructors

        public MenuService(IMenuRepository repository)                               
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            _Repository = repository;
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

            if (_Repository.Exists(menu))
            {
                throw new DataExistsException(UserSystemResource.Menu_Exists);
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
