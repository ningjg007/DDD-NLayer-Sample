using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLayer.Application.UserSystemModule.Services;

namespace NLayer.Presentation.WebHost.Areas.UserSystem.Controllers
{
    public class RoleController : Controller
    {
        IRoleService _roleService;

        
        #region Constructor

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #endregion

        // GET: UserSystem/Role
        public ActionResult Index()
        {
            var list = _roleService.FindAll();
            return View(list);
        }
    }
}