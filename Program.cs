﻿namespace Wallpaper10CnC
{
    using System;

    public class Program
    {
        private static string Username => @"<username>";
        private static string SourcePath => @"C:\Users\" + Username + @"\AppData\Local\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets";
        private static string TargetPath => @"C:\Users\" + Username + @"\Pictures";

        public static void Main(string[] args)
        {
            var wpm = new WallpaperManager();

            Console.WriteLine("Starte den Kopier und Vergleichsprozess.");

            var source = wpm.GetSourcePictures(SourcePath);
            var wallpapersToCopy = wpm.Compare(source, TargetPath);

            wpm.Transfer(wallpapersToCopy, TargetPath);
                        
            Console.WriteLine("Kopiervorgang beendet");

            // Doubletten prüfung für Zielordner

            Console.WriteLine("Doubletten entfernt");

            Console.ReadLine();
        }
    }
}