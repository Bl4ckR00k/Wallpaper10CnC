namespace Wallpaper10CnC
{
    using System;
    using System.IO;

    using Wallpaper10CnC.classes;

    public static class Program
    {
        private static string _username;

        private static string _sourcePath;

        private static string _targetPath;

        public static void Main()
        {
            GetConfiguration();

            Console.WriteLine("Starte den Kopier- und Vergleichsprozess.");

            try
            {
                var source = WallpaperManager.GetSourcePictures(_sourcePath);
                var wallpapersToCopy = WallpaperManager.Compare(source, _targetPath);
                WallpaperManager.CopyWallpapersToTarget(wallpapersToCopy, _targetPath);

                var doublettes = WallpaperManager.CompareEach(_targetPath);
                WallpaperManager.DeleteDoubleWallpapers(doublettes);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Zugriff auf das Dateisystem derzeit nicht möglich.");
            }
            finally
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Programm wird beendet.");
                System.Threading.Thread.Sleep(1000);
            }
        }

        private static void GetConfiguration()
        {
            _username = Properties.Settings.Default.Username;

            var source = Properties.Settings.Default.SourcePath;
            _sourcePath = source.Contains("@@") ? source.Replace("@@Username", _username) : source;

            var target = Properties.Settings.Default.TargetPath;
            _targetPath = target.Contains("@@") ? target.Replace("@@Username", _username) : target;
        }
    }
}