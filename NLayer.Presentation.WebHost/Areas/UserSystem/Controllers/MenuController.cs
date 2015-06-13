using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Application.UserSystemModule.Services;
using NLayer.Presentation.WebHost.Models;
using PagedList;

namespace NLayer.Presentation.WebHost.Areas.UserSystem.Controllers
{
    public class MenuController : UserSystemBaseController
    {
        IMenuService _menuService;

        #region Constructor

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        #endregion

        public ActionResult Index(string module,string menuName, int? page)
        {
            var list = _menuService.FindBy(module,menuName, page.HasValue ? page.Value : 1, DisplayExtensions.DefaultPageSize);

            ViewBag.UserName = menuName;

            return View(list);
        }

        public ActionResult EditMenu(Guid? id)
        {
            var menu = id.HasValue ? _menuService.FindBy(id.Value) : new MenuDTO();
            return View(menu);
        }

        [HttpPost]
        public ActionResult SearchMenu(string menuName)
        {
            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = false,
                RedirectUrl = Url.Action("Index", new
                {
                    menuName,
                })
            });
        }

        public ActionResult MenuPermissionList(Guid menuId)
        {
            var menu = _menuService.FindBy(menuId) ?? new MenuDTO();

            var list =  menu.Permissions;

            ViewBag.MenuName = menu.Name;
            ViewBag.MenuId = menuId;

            return View(list);
        }

        public ActionResult EditMenuPermission(Guid menuId, Guid? id)
        {
            var menu = _menuService.FindBy(menuId) ?? new MenuDTO();
            var permission = id.HasValue ?
                menu.Permissions.FirstOrDefault(x => x.Id == id) : new PermissionDTO();

            ViewBag.MenuName = menu.Name;
            ViewBag.MenuId = menuId;

            return View(permission);
        }

        [HttpPost]
        public ActionResult EditMenuPermission(Guid menuId, PermissionDTO permission, Guid? id)
        {
            var menu = _menuService.FindBy(menuId) ?? new MenuDTO();
            if (!id.HasValue)
            {
                menu.Permissions.Add(permission);
            }
            else
            {
                permission.Id = id.Value;
                var oldPermission = menu.Permissions.FirstOrDefault(x => x.Id == permission.Id);
                if (oldPermission != null)
                {
                    permission.Created = oldPermission.Created;
                    menu.Permissions.Remove(oldPermission);
                }
                menu.Permissions.Add(permission);
            }
            _menuService.UpdatePermission(menu);

            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = false,
                RedirectUrl = Url.Action("MenuPermissionList", new { menuId = menuId })
            });
        }

        public ActionResult RemoveMenuPermission(Guid menuId, Guid id)
        {
            var menu = _menuService.FindBy(menuId) ?? new MenuDTO();
            var permission = menu.Permissions.FirstOrDefault(x => x.Id == id);
            if (permission != null)
            {
                menu.Permissions.Remove(permission);
                _menuService.UpdatePermission(menu);
            }

            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = false,
                RedirectUrl = Url.Action("MenuPermissionList", new { menuId = menuId })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditMenu(MenuDTO menu)
        {
            menu.Permissions = new Collection<PermissionDTO>();
            if (menu.Id == Guid.Empty)
            {
                _menuService.Add(menu);
            }
            else
            {
                _menuService.Update(menu);
            }

            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = false,
                RedirectUrl = Url.Action("Index")
            });
        }

        public ActionResult RemoveMenu(Guid id)
        {
            _menuService.Remove(id);

            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = false,
                RedirectUrl = Url.Action("Index")
            }, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /UserSystem/User/
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}