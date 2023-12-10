using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory
{
    public static class HtmlHelpers
    {

        public static string IsSelected(this IHtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string PageClass(this IHtmlHelper htmlHelper)
        {
            string currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static string IsInventorySelected(this IHtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "menu-is-opening menu-open";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            if (currentController == "ProductTypes" || currentController == "Products")
            {
                return cssClass;
            }
            else
            {
                return "";
            }

        }
        public static string IsPurchaseSelected(this IHtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "menu-is-opening menu-open";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            if (currentController == "ProductTypes" || currentController == "Stock")
            {
                return cssClass;
            }
            else
            {
                return "";
            }

        }
        public static string IsSalesSelected(this IHtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "menu-is-opening menu-open";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            if (currentController == "ProductTypes" || currentController == "Stock")
            {
                return cssClass;
            }
            else
            {
                return "";
            }

        }
        public static string IsConfigSelected(this IHtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "menu-is-opening menu-open";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            if (currentController == "ProductTypes" || currentController == "Stock")
            {
                return cssClass;
            }
            else
            {
                return "";
            }

        }

    }
}
