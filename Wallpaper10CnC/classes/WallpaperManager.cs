namespace Wallpaper10CnC
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;

    public class WallpaperManager
    {
        private static int imageWidth;
        private static int imageHeight;
        private static PictureFormat imageFormat;

        public WallpaperManager()
        {
            imageWidth = 1920;
            imageHeight = 1080;
            imageFormat = PictureFormat.Landscape;
        }

        public WallpaperManager(PictureFormat imageFormat, int width, int height)
        {
            imageWidth = width;
            imageHeight = height;
            WallpaperManager.imageFormat = imageFormat;
        }

        public IEnumerable<Wallpaper> Compare(IEnumerable<Wallpaper> source, string targetPath)
        {
            var sourceHashs = source.Select(s => new Wallpaper(s.Path, s.FileName, GenerateHash(s.Path), s.Extension)).ToList();

            var targetHashs = GetFilesFormPath(targetPath).Select(s => new Wallpaper(s, string.Empty, GenerateHash(s))).ToList();

            return sourceHashs.Except(targetHashs, new HashComparer()).ToList();
        }

        public IEnumerable<Wallpaper> CompareEach(string comparePath)
        {
            var compares = GetFilesFormPath(comparePath).Select(s => new Wallpaper(s, GetFileName(s), GenerateHash(s))).ToList();
            List<Wallpaper> result = new List<Wallpaper>();

            foreach(var paper in compares)
            {
                result.AddRange(compares.Where(w => w.HashCode == paper.HashCode && w.FileName != paper.FileName).Select( s => new Wallpaper(s.Path, s.FileName, s.HashCode)));
            }

            return result;
        }

        public void Transfer(IEnumerable<Wallpaper> wallpapers, string targetPath)
        {
            wallpapers.ToList().ForEach(p => File.Copy(p.Path, targetPath + "\\" + p.FileName + p.Extension));
        }

        public void DeleteWallpaperRange(IEnumerable<Wallpaper> wallpapers)
        {
            foreach (var paper in wallpapers)
            {
                DeleteWallpaper(paper);
            }
        }

        public void DeleteWallpaper(Wallpaper wallpaper)
        {
            File.Delete(wallpaper.Path);
        }
        
        public IEnumerable<Wallpaper> GetSourcePictures(string path)
        {
            var sourcePictures = Directory.GetFiles(path).ToList();
            var source = new List<Wallpaper>();

            foreach (var filepath in sourcePictures)
            {
                var file = new Wallpaper(filepath);
                file.GetImageFormat(File.ReadAllBytes(filepath));

                file.FileName = filepath.Split('\\').Last();

                if (file.Extension != null)
                {
                    source.Add(file);
                }
            }

            var countComplete = source.Count;

            List<Wallpaper> returnValue;

            switch (imageFormat)
            {
                case PictureFormat.Landscape:
                    returnValue = source
                                    .Where(p => Image.FromFile(p.Path).Width > Image.FromFile(p.Path).Height 
                                             && Image.FromFile(p.Path).Width == imageWidth)
                                    .ToList();
                    Console.WriteLine("Importiere nur Querformat-Wallpaper ({0}).", returnValue.Count);
                    return returnValue;

                case PictureFormat.Portrait:
                    returnValue = source
                                    .Where(p => Image.FromFile(p.Path).Width < Image.FromFile(p.Path).Height
                                             && Image.FromFile(p.Path).Height == imageHeight)
                                    .ToList();
                    Console.WriteLine("Importiere nur Hochformat-Wallpaper ({0}).", returnValue.Count);
                    return returnValue;

                case PictureFormat.Any:
                    returnValue = source
                                    .Where(p => Image.FromFile(p.Path).Height == imageHeight || Image.FromFile(p.Path).Height == imageWidth)
                                    .ToList();
                    Console.WriteLine("Importiere nur Wallpaper ({0}).", returnValue.Count);
                    return returnValue;

                default:
                    Console.WriteLine("Importiere alle Bilder ({0}).", countComplete);
                    return source.ToList();
            }
        }

        private static string GenerateHash(string file)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file))
                {
                    var md5Hash = md5.ComputeHash(stream);

                    return BitConverter.ToString(md5Hash).Replace("-", string.Empty).ToLower();
                }
            }
        }

        private static string GetFileName(string Path)
        {
            return Path.Split('\\').Last();
        }

        private static string[] GetFilesFormPath(string path)
        {
            return !Directory.Exists(path) ? null : Directory.GetFiles(path);
        }

        private class HashComparer : IEqualityComparer<Wallpaper>
        {
            public bool Equals(Wallpaper x, Wallpaper y) => x.HashCode == y.HashCode;

            public int GetHashCode(Wallpaper obj) => obj.HashCode.GetHashCode();
        }
    }
}