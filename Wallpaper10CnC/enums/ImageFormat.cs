namespace Wallpaper10CnC
{
    using System.ComponentModel;

    public enum ImageFormat
    {
        [Description(".bmp")]
        BMP,

        [Description(".jpg")]
        JEPG,

        [Description(".gif")]
        GIF,

        [Description(".tiff")]
        TIFF,

        [Description(".png")]
        PNG,

        [Description("")]
        Unknown
    }
}
