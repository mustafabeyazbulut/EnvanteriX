using EnvanteriX.WebUI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnvanteriX.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class BaseController : Controller
    {
        protected readonly IApiClientService _apiClientService;

        public BaseController(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // GET istekleri için önceki sayfayı sakla
            if (HttpContext.Request.Method == "GET")
            {
                TempData["PreviousUrl"] = HttpContext.Request.Path + HttpContext.Request.QueryString;
                var referer = HttpContext.Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(referer))
                {
                    TempData["PreviousUrl"] = referer;
                }
            }
            base.OnActionExecuting(context);
        }
        public IActionResult RedirectAfterPost(bool isError, object model = null, string fallbackAction = "Index", string fallbackController = null)
        {
            if (isError)
            {
                if (Request.Method == "GET")
                {
                    // GET isteği ise önceki sayfaya dön
                    if (TempData["PreviousUrl"] != null)
                    {
                        return Redirect(TempData["PreviousUrl"].ToString());
                    }
                    return fallbackController == null
                        ? RedirectToAction(fallbackAction)
                        : RedirectToAction(fallbackAction, fallbackController);
                }
                else if (Request.Method == "POST")
                {
                    // POST isteği ise mevcut sayfada kal ve modeli gönder
                    return View(model);
                }
            }
            else
            {
                // Hata yoksa önceki sayfaya dön
                if (TempData["PreviousUrl"] != null)
                {
                    return Redirect(TempData["PreviousUrl"].ToString());
                }
            }

            // Fallback
            return fallbackController == null
                ? RedirectToAction(fallbackAction)
                : RedirectToAction(fallbackAction, fallbackController);
        }

    }
}
