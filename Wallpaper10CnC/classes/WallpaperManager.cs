namespace Wallpaper10CnC.classes
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;

    public static class WallpaperManager
    {
        private static int _imageWidth = 1920;
        private static int _imageHeight = 1080;
        private static PictureFormat _imageFormat = PictureFormat.Landscape;

        public static void Initialize(PictureFormat imageFormat, int imageWidth, int imageHeight)
        {
            _imageFormat = imageFormat;
            _imageWidth = imageWidth;
            _imageHeight = imageHeight;
        }

        public static List<Wallpaper> Compare(IEnumerable<Wallpaper> source, string targetPath)
        {
            var sourceHashs = source.Select(s => new Wallpaper(s.Path, s.FileName, GenerateHash(s.Path), s.Extension)).ToList();

            var targetHashs = GetFilesFormPath(targetPath).Select(s => new Wallpaper(s, string.Empty, GenerateHash(s))).ToList();

            return sourceHashs.Except(targetHashs, new HashComparer()).ToList();
        }

        public static List<Wallpaper> CompareEach(string comparePath)
        {
            var compares = GetFilesFormPath(comparePath).Select(s => new Wallpaper(s, GetFileName(s), GenerateHash(s))).ToList();
            var result = new List<Wallpaper>();

            foreach(var paper in compares)
            {
                if(result.All(p => p.FileName != paper.FileName))
                {
                    result.AddRange(compares.Where(w => w.HashCode == paper.HashCode && w.FileName != paper.FileName).Select(s => new Wallpaper(s.Path, s.FileName, s.HashCode)));
                }
            }

            return result;
        }

        public static void CopyWallpapersToTarget(List<Wallpaper> wallpapers, string targetPath)
        {
            foreach (var p in wallpapers)
            {
                try
                {
                    File.Copy(p.Path, targetPath + "\\" + p.FileName + p.Extension);
                }
                catch (FileNotFoundException)
                {
                    // tritt sporadisch auf, wenn die Aktualisierung des Microsoft-Ordners noch 
                    // nicht abgeschlossen, das Programm aber bereits in der Ausführung ist.
                }
            }

            Console.WriteLine("Importierte Wallpapers: {0}", wallpapers.Count);
        }

        public static void DeleteDoubleWallpapers(List<Wallpaper> wallpapers)
        {
            foreach (var paper in wallpapers)
            {
                DeleteWallpaper(paper);
            }
            
            Console.WriteLine("Anzahl Doubletten: " + wallpapers.Count);
        }

        private static void DeleteWallpaper(Wallpaper wallpaper)
        {
            File.Delete(wallpaper.Path);
        }
        
        public static IEnumerable<Wallpaper> GetSourcePictures(string path)
        {
            var sourcePictures = Directory.GetFiles(path).ToList();
            var source = new List<Wallpaper>();

            foreach (var filepath in sourcePictures)
            {
                var file = new Wallpaper(filepath);
                try
                {

                    file.GetImageFormat(File.ReadAllBytes(filepath));

                    file.FileName = filepath.Split('\\').Last();

                    if (file.Extension != null)
                    {
                        source.Add(file);
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine($"Auf das Bild '{filepath}' konnte nicht zugegriffen werden.");
                }
            }

            var countComplete = source.Count;

            List<Wallpaper> returnValue;

            switch (_imageFormat)
            {
                case PictureFormat.Landscape:
                    returnValue = source
                                    .Where(p => Image.FromFile(p.Path).Width > Image.FromFile(p.Path).Height 
                                             && Image.FromFile(p.Path).Width == _imageWidth)
                                    .ToList();
                    Console.WriteLine("Gefundene Querformat-Wallpapers: {0}", returnValue.Count);
                    return returnValue;

                case PictureFormat.Portrait:
                    returnValue = source
                                    .Where(p => Image.FromFile(p.Path).Width < Image.FromFile(p.Path).Height
                                             && Image.FromFile(p.Path).Height == _imageHeight)
                                    .ToList();
                    Console.WriteLine("Gefundene Hochformat-Wallpapers: {0}", returnValue.Count);
                    return returnValue;

                case PictureFormat.Any:
                    returnValue = source
                                    .Where(p => Image.FromFile(p.Path).Height == _imageHeight 
                                             || Image.FromFile(p.Path).Height == _imageWidth)
                                    .ToList();
                    Console.WriteLine("Gefundene Wallpapers: {0}", returnValue.Count);
                    return returnValue;

                default:
                    Console.WriteLine("Gefundene Bilddateien: {0}", countComplete);
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

        private static string GetFileName(string path)
        {
            return path.Split('\\').Last();
        }

        private static IEnumerable<string> GetFilesFormPath(string path)
        {
            return !Directory.Exists(path) ? null : Directory.GetFiles(path);
        }

        private class HashComparer : IEqualityComparer<Wallpaper>
        {
            public bool Equals(Wallpaper x, Wallpaper y) => y != null && (x != null && x.HashCode == y.HashCode);

            public int GetHashCode(Wallpaper obj) => obj.HashCode.GetHashCode();
        }
    }
}