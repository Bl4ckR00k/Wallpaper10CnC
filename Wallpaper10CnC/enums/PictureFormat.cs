namespace Wallpaper10CnC
{
    using System.ComponentModel;

    public enum PictureFormat
    {
        [Description("Alle Dateien")]
        None,

        [Description("Hoch- und Querformat")]
        Any,

        [Description("Hochformat")]
        Landscape,

        [Description("Querformat")]
        Portrait
    }
}