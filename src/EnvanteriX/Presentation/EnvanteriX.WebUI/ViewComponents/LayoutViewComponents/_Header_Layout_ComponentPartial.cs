using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.ViewComponents.LayoutViewComponents
{
    public class _Header_Layout_ComponentPartial : ViewComponent
    {
        public _Header_Layout_ComponentPartial()
        {
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
