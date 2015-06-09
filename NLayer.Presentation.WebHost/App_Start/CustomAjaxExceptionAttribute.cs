using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;
using NLayer.Presentation.WebHost.Models;

namespace NLayer.Presentation.WebHost
{
    public class CustomAjaxExceptionAttribute : FilterAttribute, IExceptionFilter   //HandleErrorAttribute
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CustomAjaxExceptionAttribute));

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled
                || !filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                return;
            }

            var ex = filterContext.Exception;

            filterContext.Result = new JsonResult
            {
                Data = new AjaxResponse
                {
                    Succeeded = false,
                    ErrorMessage = ex.Message
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            Log.Error(ex.GetIndentedExceptionLog());

            //写入日志 记录
            filterContext.ExceptionHandled = true; //设置异常已经处理
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}
