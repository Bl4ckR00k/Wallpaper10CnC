namespace Wallpaper10CnC
{
    using System;

    public class Program
    {
        private static string username;

        private static string sourcePath;

        private static string targetPath;

        public static void Main(string[] args)
        {
            GetConfiguration();

            // var wpm = new WallpaperManager(PictureFormat.Landscape, 1920, 1080);
            var wpm = new WallpaperManager();

            Console.WriteLine("Starte den Kopier und Vergleichsprozess.");

            var source = wpm.GetSourcePictures(sourcePath);
            var wallpapersToCopy = wpm.Compare(source, targetPath);
            wpm.CopyWallpapersToTarget(wallpapersToCopy, targetPath);

            var doublettes = wpm.CompareEach(targetPath);
            wpm.DeleteDoubleWallpapers(doublettes); 

            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Programm wird beendet.");
            System.Threading.Thread.Sleep(1000);
        }

        private static void GetConfiguration()
        {
            username = Properties.Settings.Default.Username.ToString();

            var source = Properties.Settings.Default.SourcePath.ToString();
            sourcePath = source.Contains("@@") ? source.Replace("@@Username", username) : source;

            var target = Properties.Settings.Default.TargetPath.ToString();
            targetPath = target.Contains("@@") ? target.Replace("@@Username", username) : target;
        }
    }
}