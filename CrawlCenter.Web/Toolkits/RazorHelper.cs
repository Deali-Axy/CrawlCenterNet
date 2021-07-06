using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CrawlCenter.Web.ToolKits {
    public static class RazorHelper {
        public static string GetCurrentControllerName(ViewContext viewContext) {
            return viewContext.RouteData.Values["controller"] == null ? "" : viewContext.RouteData.Values["controller"].ToString();
        }

        public static string GetCurrentActionName(ViewContext viewContext) {
            return viewContext.RouteData.Values["action"] == null ? "" : viewContext.RouteData.Values["action"].ToString();
        }

        public static string MenuHighlight(ViewContext viewContext, string controllerName) {
            return controllerName == GetCurrentControllerName(viewContext) ? "active" : "";
        }

        public static string SideMenuHighlight(ViewContext viewContext, string actionName) {
            return actionName == GetCurrentActionName(viewContext) ? "active" : "";
        }

        public static string SrOnly(ViewContext viewContext, string controllerName) {
            return controllerName == GetCurrentControllerName(viewContext) ? " <span class=\"sr-only\">(current)</span>" : "";
        }
    }
}