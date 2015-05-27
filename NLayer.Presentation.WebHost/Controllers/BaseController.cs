using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace NLayer.Presentation.WebHost.Controllers
{
    public class BaseController : Controller
    {
        protected static readonly ILog Log = LogManager.GetLogger(typeof(BaseController));
    }
}