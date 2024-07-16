using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QLHocSinh_LT.Filters
{
    public class LoginFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity == null)
                return;
            var controller = context.Controller as Controller;
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // Kiểm tra nếu người dùng đang truy cập trang đăng nhập để tránh vòng lặp chuyển hướng
                var controllerName = context.RouteData.Values["controller"]?.ToString();
                var actionName = context.RouteData.Values["action"]?.ToString();

                if (!(controllerName == "Authorized" && actionName == "Index"))
                {
                    if (controller != null)
                    {
                        controller.TempData["LoginRequired"] = "Bạn phải đăng nhập để truy cập trang này.";
                    }
                    context.Result = new RedirectToActionResult("Index", "Authorized", null);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do nothing
        }
    }
}
