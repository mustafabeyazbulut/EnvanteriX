using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.ViewComponents.LayoutViewComponents
{
    public class _LeftSideBar_Layout_ComponentPartial : ViewComponent
    {
        public _LeftSideBar_Layout_ComponentPartial()
        {
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
