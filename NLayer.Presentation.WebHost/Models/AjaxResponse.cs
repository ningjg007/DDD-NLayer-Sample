using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLayer.Presentation.WebHost.Models
{
    public class AjaxResponse
    {
        public AjaxResponse()
        {
            this.ShowMessage = true;
        }

        public string RedirectUrl { get; set; }

        public string Message { get; set; }

        public bool ShowMessage { get; set; }

        public bool Succeeded { get; set; }

        public string ErrorMessage { get; set; }
    }
}