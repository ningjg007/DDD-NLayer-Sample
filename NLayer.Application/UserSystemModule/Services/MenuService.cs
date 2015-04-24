using System;
using NLayer.Application.UserSystemModule.Converters;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.MenuAgg;
using NLayer.Infrastructure.Entity;
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

        public void Remove(MenuDTO menuDTO)
        {
            var menu = _Repository.Get(menuDTO.Id);

            if (menu != null) //if exist
            {
                _Repository.Remove(menu);

                //commit unit of work
                _Repository.UnitOfWork.Commit();
            }
            else
            {
                // Not Exists
            }
        }

        public IPagedList<MenuDTO> FindBy(string module, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
