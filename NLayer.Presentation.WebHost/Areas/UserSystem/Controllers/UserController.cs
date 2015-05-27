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
    public class UserController : UserSystemBaseController
    {
        IUserService _userService;



        #region Constructor

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        public ActionResult Index(string userName, int? page)
        {
            var list = _userService.FindBy(userName, page.HasValue ? page.Value : 1, DisplayExtensions.DefaultPageSize);

            ViewBag.UserName = userName;

            return View(list);
        }

        public ActionResult EditUser(Guid? id)
        {
            var user = id.HasValue ? _userService.FindBy(id.Value) : new UserDTO();
            return View(user);
        }

        [HttpPost]
        public ActionResult SearchUser(string userName)
        {
            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = false,
                RedirectUrl = Url.Action("Index", new
                {
                    userName,
                })
            });
        }

        public ActionResult UserList(Guid? userId, string name)
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditUser(UserDTO user)
        {
            if (user.Id == Guid.Empty)
            {
                _userService.Add(user);
            }
            else
            {
                _userService.Update(user);
            }

            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = false,
                RedirectUrl = Url.Action("Index")
            });
        }

        public ActionResult RemoveUser(Guid id)
        {
            _userService.Remove(id);

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