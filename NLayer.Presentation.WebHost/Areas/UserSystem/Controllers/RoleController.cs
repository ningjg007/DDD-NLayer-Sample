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
    public class RoleController : UserSystemBaseController
    {
        IRoleService _roleService;

        IRoleGroupService _roleGroupService;

        
        #region Constructor

        public RoleController(IRoleService roleService, IRoleGroupService roleGroupService)
        {
            _roleService = roleService;
            _roleGroupService = roleGroupService;
        }

        #endregion

        // GET: UserSystem/Role
        public ActionResult Index(string groupName, int? page)
        {
            var list = _roleGroupService.FindBy(groupName, page.HasValue ? page.Value : 1, DisplayExtensions.DefaultPageSize);

            ViewBag.GroupName = groupName;

            return View(list);
        }

        [HttpPost]
        public ActionResult SearchGroup(string groupName)
        {
            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = false,
                RedirectUrl = Url.Action("Index", new
                {
                    groupName,
                })
            });
        }

        public ActionResult RoleList(Guid? groupId, string name)
        {
            return View();
        }

        public ActionResult EditRole(Guid? id)
        {
            var role = id.HasValue ? _roleService.FindBy(id.Value) : new RoleDTO();
            return View(role);
        }

        public ActionResult EditGroup(Guid? id)
        {
            var group = id.HasValue ? _roleGroupService.FindBy(id.Value) : new RoleGroupDTO();
            return View(group);
        }


        [HttpPost]
        public ActionResult EditGroup(RoleGroupDTO roleGroup)
        {
            if (roleGroup.Id == Guid.Empty)
            {
                _roleGroupService.Add(roleGroup);
            }
            else
            {
                _roleGroupService.Update(roleGroup);
            }

            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = false,
                RedirectUrl = Url.Action("Index")
            });
        }

        public ActionResult RemoveGroup(Guid id)
        {
            _roleGroupService.Remove(id);

            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = false,
                RedirectUrl = Url.Action("Index")
            }, JsonRequestBehavior.AllowGet);
        }
        
    }
}