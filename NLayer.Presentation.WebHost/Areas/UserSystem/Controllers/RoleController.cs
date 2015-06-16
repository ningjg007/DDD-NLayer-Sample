using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLayer.Application.Modules;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Application.UserSystemModule.Services;
using NLayer.Infrastructure.Authorize;
using NLayer.Presentation.WebHost.Models;
using NLayer.Presentation.WebHost.Resources;
using PagedList;

namespace NLayer.Presentation.WebHost.Areas.UserSystem.Controllers
{
    [AuthorizeFilter]
    public class RoleController : UserSystemBaseController
    {
        IRoleService _roleService;

        IRoleGroupService _roleGroupService;

        IMenuService _menuService;

        IUserService _userService;

        
        #region Constructor

        public RoleController(IRoleService roleService, IRoleGroupService roleGroupService, IMenuService menuService,
            IUserService userService)
        {
            _roleService = roleService;
            _roleGroupService = roleGroupService;
            _menuService = menuService;
            _userService = userService;
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
        public ActionResult EditRolePermission(Guid roleId)
        {
            var menus = new List<MenuDTO>();

            var modules = NLayerModulesManager.Instance.ListAll();
            foreach (var module in modules)
            {
                menus.AddRange(_menuService.FindByModule(module.Type.ToString()));
            }

            var role = _roleService.FindBy(roleId);
            var roleGroup = _roleGroupService.FindBy(role.RoleGroupId);

            var permissions = _roleService.GetRolePermission(roleId);

            ViewBag.Modules = modules;
            ViewBag.Menus = menus;
            ViewBag.Role = role;
            ViewBag.RoleGroup = roleGroup;
            ViewBag.Permissions = permissions;

            return View();
        }

        [HttpPost]
        public ActionResult EditRolePermission(Guid roleId, List<string> permissions)
        {
            var pList = new List<Guid>();

            foreach (var s in permissions)
            {
                Guid id;
                if (Guid.TryParse(s, out id))
                {
                    pList.Add(id);
                }
            }

            if (pList.Count > 0)
            {
                _roleService.UpdateRolePermission(roleId, pList);
            }

            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = true,
                Message = CommonResource.Msg_Operate_Ok,
                RedirectUrl = string.Empty
            });
        }

        public ActionResult EditUserList(Guid groupId)
        {
            var group = _roleGroupService.FindBy(groupId);

            var allUsers = _userService.GetAllUsersIdName();
            var existsUsers = _roleGroupService.GetUsersIdName(groupId);

            ViewBag.Group = group;
            ViewBag.AllUsers = allUsers;
            ViewBag.ExistsUsers = existsUsers;

            return View();
        }

        [HttpPost]
        public ActionResult EditUserList(Guid groupId, List<string> users)
        {
            var pList = new List<Guid>();

            foreach (var s in users)
            {
                Guid id;
                if (Guid.TryParse(s, out id))
                {
                    pList.Add(id);
                }
            }

            if (pList.Count > 0)
            {
                _roleGroupService.UpdateGroupUsers(groupId, pList);
            }

            return Json(new AjaxResponse
            {
                Succeeded = true,
                ShowMessage = true,
                Message = CommonResource.Msg_Operate_Ok,
                RedirectUrl = string.Empty
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