namespace Wallpaper10CnC
{
    using System.ComponentModel;

    public enum pictureFormat
    {
        [Description("Alle Dateien")]
        none,

        [Description("Hoch- und Querformat")]
        any,

        [Description("Hochformat")]
        landscape,

        [Description("Querformat")]
        portrait
    }
}