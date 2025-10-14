using System.ComponentModel.DataAnnotations;

namespace EnvanteriX.WebUI.ViewModels.Location
{
    public class CreateLocationViewModel
    {
        public string Building { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }
    }
}
