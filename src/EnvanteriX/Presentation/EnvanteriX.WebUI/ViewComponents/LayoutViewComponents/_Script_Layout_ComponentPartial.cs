using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebUI.ViewComponents.LayoutViewComponents
{
    public class _Script_Layout_ComponentPartial : ViewComponent
    {
        public _Script_Layout_ComponentPartial()
        {
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
