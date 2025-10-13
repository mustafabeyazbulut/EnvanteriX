using System.ComponentModel;

namespace EnvanteriX.Domain.Enums
{
    public enum StatusEnum
    {
        [Description("Ürün stokta mevcut.")]
        Stokta = 1,

        [Description("Ürün şu anda kullanımda.")]
        Kullanimda = 2,

        [Description("Ürün tamirde.")]
        Tamirde = 3,

        [Description("Ürün artık kullanım dışı.")]
        KullanimDisi = 4
    }
}
