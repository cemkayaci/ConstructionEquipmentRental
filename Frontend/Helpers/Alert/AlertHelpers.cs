using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Helpers.Alert
{
    public static class AlertHelpers
    {
        public static IActionResult AlertSuccess(this IActionResult result, string title, string body)
        {
            return Alert(result, "success", title, body);
        }

        public static IActionResult AlertInfo(this IActionResult result, string title, string body)
        {
            return Alert(result, "info", title, body);
        }

        public static IActionResult AlertWarning(this IActionResult result, string title, string body)
        {
            return Alert(result, "warning", title, body);
        }

        public static IActionResult AlertDanger(this IActionResult result, string title, string body)
        {
            return Alert(result, "danger", title, body);
        }

        private static IActionResult Alert(IActionResult result, string type, string title, string body)
        {
            return new AlertResult(result, type, title, body);
        }
    }
}
