using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.ViewComponents.LayoutViewComponents
{
    public class _Head_Layout_ComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
