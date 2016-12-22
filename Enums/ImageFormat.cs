namespace Wallpaper10CnC
{
    using System.ComponentModel;

    public enum ImageFormat
    {
        [Description(".bmp")]
        bmp,
        [Description(".jpg")]
        jpeg,
        [Description(".gif")]
        gif,
        [Description(".tiff")]
        tiff,
        [Description(".png")]
        png,
        [Description("")]
        unknown
    }
}
