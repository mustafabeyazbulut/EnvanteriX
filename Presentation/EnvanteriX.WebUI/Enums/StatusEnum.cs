using System.ComponentModel;

namespace EnvanteriX.WebUI.Enums
{
    public enum StatusEnum
    {
        None = 0,      // sentinel value — dropdown'da gösterilmez
        [Description("Ürün stokta mevcut.")]
        Stokta = 1,

        [Description("Ürün şu anda kullanımda.")]
        Kullanimda = 2,

        [Description("Ürün tamirde.")]
        Tamirde = 3,

        [Description("Ürün artık kullanım dışı.")]
        KullanimDisi = 4,

        [Description("Ürün hurda/pasif olarak işaretlendi.")]
        Pasif = 5
    }
}
