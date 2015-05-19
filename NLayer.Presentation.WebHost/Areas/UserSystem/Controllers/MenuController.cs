using System;
using System.Collections.Generic;
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

        public ActionResult MenuList(Guid? menuId, string name)
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditMenu(MenuDTO menu)
        {
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