using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NLayer.Application.Modules;
using PagedList.Mvc;

namespace NLayer.Presentation.WebHost
{
    public static class DisplayExtensions
    {
        public static string Display(this DateTime dateTime)
        {
            var t = dateTime.ToLocalTime();

            return t.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public static string Display(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.Display() : "";
        }

        public static string DisplayDate(this DateTime dateTime)
        {
            var t = dateTime.ToLocalTime();

            return t.ToString("yyyy/MM/dd");
        }

        public static string DisplayDate(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.DisplayDate() : "";
        }

        public static string DisplayDateHourMinute(this DateTime dateTime)
        {
            var t = dateTime.ToLocalTime();

            return t.ToString("yyyy/MM/dd HH:mm");
        }

        public static string DisplayDateHourMinute(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.DisplayDateHourMinute() : "";
        }

        public static string Display(this Decimal value)
        {
            return value.ToString("#,##0.00");
        }

        public static string Display(this Decimal? value)
        {
            return value.HasValue ? value.Value.Display() : "";
        }

        public static PagedListRenderOptions PagedListRenderOptions
        {
            get
            {
                return new PagedListRenderOptions
                {
                    LinkToFirstPageFormat = "首页",
                    LinkToNextPageFormat = "下页",
                    LinkToPreviousPageFormat = "上页",
                    LinkToLastPageFormat = "末页",
                    MaximumPageNumbersToDisplay = 5,
                    DisplayEllipsesWhenNotShowingAllPageNumbers = false,
                };
            }
        }

        public static int DefaultPageSize = 15;

        public static string Display(this NLayerModulesType value)
        {
            return NLayerModulesManager.Instance.GetModulesName(value);
        }
    }
}