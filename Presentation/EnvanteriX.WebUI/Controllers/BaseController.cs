using EnvanteriX.WebUI.Attributes;
using EnvanteriX.WebUI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnvanteriX.WebUI.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly IApiClientService _apiClientService;
        protected readonly ILogger<BaseController> _logger;

        public BaseController(IApiClientService apiClientService, ILogger<BaseController> logger)
        {
            _apiClientService = apiClientService;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // 🔹 1. SkipBaseActionFilter attribute'u var mı kontrol et
            var hasSkipAttribute = context.ActionDescriptor.EndpointMetadata
                .OfType<SkipBaseActionFilterAttribute>()
                .Any();

            if (hasSkipAttribute)
            {
                _logger.LogDebug("OnActionExecuting atlandı: {Action}", context.ActionDescriptor.DisplayName);
                base.OnActionExecuting(context);
                return;
            }

            // 🔹 2. Normal işlem (senin mevcut kodun)
            if (HttpContext.Request.Method == "GET")
            {
                var referer = HttpContext.Request.Headers["Referer"].ToString();
                var currentPath = HttpContext.Request.Path.ToString();

                if (!string.IsNullOrEmpty(referer))
                {
                    if (!referer.Contains(currentPath))
                    {
                        TempData["PreviousUrl"] = referer;
                        _logger.LogDebug("PreviousUrl güncellendi (GET - Referer): {Url}", referer);
                    }
                    else
                    {
                        _logger.LogDebug("PreviousUrl güncellenmedi - Aynı sayfa: {Url}", referer);
                    }
                }
                else
                {
                    if (TempData["PreviousUrl"] == null)
                    {
                        var currentUrl = HttpContext.Request.Path + HttpContext.Request.QueryString;
                        TempData["PreviousUrl"] = currentUrl;
                        _logger.LogDebug("PreviousUrl güncellendi (GET - Current): {Url}", currentUrl);
                    }
                }
            }
            else if (HttpContext.Request.Method == "POST")
            {
                TempData.Keep("PreviousUrl");
                _logger.LogDebug("POST isteği - PreviousUrl korundu: {Url}", TempData["PreviousUrl"]);
            }

            base.OnActionExecuting(context);
        }

        protected void SavePreviousUrlBeforePost()
        {
            // POST action'ında, hata oluşmadan ÖNCE bu metodu çağır
            // Bu sayede önceki sayfa URL'i kesinlikle korunur
            var referer = HttpContext.Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                TempData["PreviousUrl"] = referer;
                _logger.LogDebug("PreviousUrl POST öncesi kaydedildi: {Url}", referer);
            }
        }

        public IActionResult RedirectAfterPost(bool isError, object model = null, string fallbackAction = "Index", string fallbackController = null)
        {
            if (isError)
            {
                if (Request.Method == "POST")
                {
                    // POST isteği ise mevcut sayfada kal ve modeli gönder
                    TempData.Keep("PreviousUrl");
                    _logger.LogInformation("Hatalı POST: Aynı sayfada kalınıyor. PreviousUrl: {Url}", TempData["PreviousUrl"]);
                    return View(model);
                }
                else if (Request.Method == "GET")
                {
                    if (TempData["PreviousUrl"] != null)
                    {
                        var previousUrl = TempData["PreviousUrl"].ToString();
                        TempData.Keep("PreviousUrl");
                        _logger.LogInformation("Hatalı GET: PreviousUrl'e yönlendiriliyor: {Url}", previousUrl);
                        return Redirect(previousUrl);
                    }
                    _logger.LogInformation("Hatalı GET: Fallback yönlendirme: {Action}/{Controller}", fallbackAction, fallbackController);
                    return fallbackController == null
                        ? RedirectToAction(fallbackAction)
                        : RedirectToAction(fallbackAction, fallbackController);
                }
            }
            else
            {
                // Başarılı işlem
                if (TempData["PreviousUrl"] != null)
                {
                    var previousUrl = TempData["PreviousUrl"].ToString();
                    TempData.Remove("PreviousUrl");
                    _logger.LogInformation("Başarılı işlem: PreviousUrl'e yönlendiriliyor: {Url}", previousUrl);
                    return Redirect(previousUrl);
                }
                _logger.LogInformation("Başarılı işlem: Fallback yönlendirme: {Action}/{Controller}", fallbackAction, fallbackController);
            }

            // Fallback
            return fallbackController == null
                ? RedirectToAction(fallbackAction)
                : RedirectToAction(fallbackAction, fallbackController);
        }
    }
}