using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using NLayer.Application.Modules;
using NLayer.Infrastructure.Helper;

namespace NLayer.Presentation.WebHost
{
    public static class NLayerHtmlHelper
    {
        public static List<SelectListItem> GetItemList(this List<NLayerModules> list,
            string includeOption,
            string defValue = "")
        {
            var result = list.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Type.ToString(),
                Selected = x.Type.ToString().EqualsIgnoreCase(defValue)
            }).ToList();

            if (includeOption.NotNullOrBlank())
            {
                result.Insert(0, new SelectListItem() { Text = includeOption, Value = "" });
            }

            return result;
        }
    }
}