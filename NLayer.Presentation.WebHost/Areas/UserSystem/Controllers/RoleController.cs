using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Application.UserSystemModule.Services;
using NLayer.Infrastructure.Authorize;
using NLayer.Presentation.WebHost.Models;
using PagedList;

namespace NLayer.Presentation.WebHost.Areas.UserSystem.Controllers
{
    [AuthorizeFilter]
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

        //[Permission("UserSystem:RoleList")]
        public ActionResult RoleList(Guid groupId, string name, int? page)
        {
            var list = _roleService.FindBy(groupId, name, page.HasValue ? page.Value : 1, DisplayExtensions.DefaultPageSize);

            var group = _roleGroupService.FindBy(groupId);

            ViewBag.GroupName = group == null ? string.Empty : group.Name;
            ViewBag.GroupId = groupId;
            ViewBag.Name = name;

            return View(list);
        }

        [HttpPost]
        public ActionResult SearchRoleList(Guid groupId, string name)
        {
            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = false,
                RedirectUrl = Url.Action("RoleList", new
                {
                    groupId,
                    name
                })
            });
        }

        public ActionResult EditRole(Guid groupId, Guid? id)
        {
            var role = id.HasValue ? _roleService.FindBy(id.Value) : new RoleDTO();

            var group = _roleGroupService.FindBy(groupId);

            ViewBag.GroupName = group == null ? string.Empty : group.Name;
            ViewBag.GroupId = groupId;

            return View(role);
        }

        [HttpPost]
        public ActionResult EditRole(Guid groupId, RoleDTO role)
        {
            role.RoleGroupId = groupId;
            if (role.Id == Guid.Empty)
            {
                _roleService.Add(role);
            }
            else
            {
                _roleService.Update(role);
            }

            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = false,
                RedirectUrl = Url.Action("RoleList", new { groupId = groupId })
            });
        }

        public ActionResult RemoveRole(Guid groupId, Guid id)
        {
            _roleService.Remove(id);

            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = false,
                RedirectUrl = Url.Action("RoleList", new { groupId = groupId })
            }, JsonRequestBehavior.AllowGet);
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