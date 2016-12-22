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
        private static int _imageWidth;
        private static int _imageHeight;
        private static pictureFormat _imageFormat;

        public WallpaperManager()
        {
            _imageWidth = 1920;
            _imageHeight = 1080;
            _imageFormat = pictureFormat.landscape;
        }

        public WallpaperManager(pictureFormat imageFormat, int width, int height)
        {
            _imageWidth = width;
            _imageHeight = height;
            _imageFormat = imageFormat;
        }

        public IEnumerable<Wallpaper> Compare(IEnumerable<Wallpaper> source, string targetPath)
        {
            var sourceHashs = source.Select(s => new Wallpaper(s.Path, s.FileName, GenerateHash(s.Path))).ToList();

            var targetHashs = GetFilesFormPath(targetPath).Select(s => new Wallpaper(s, string.Empty, GenerateHash(s))).ToList();

            return sourceHashs.Except(targetHashs, new HashComparer());
        }

        public void Transfer(IEnumerable<Wallpaper> wallpapers, string targetPath)
        {
            wallpapers.ToList().ForEach(p => File.Copy(p.Path, targetPath + "\\" + p.FileName + p.Extension));
        }

        public IEnumerable<Wallpaper> GetSourcePictures(string path)
        {
            var sourcePictures = Directory.GetFiles(path).ToList();
            var source = new List<Wallpaper>();

            foreach(var filepath in sourcePictures)
            {
                var file = new Wallpaper(filepath);
                file.GetImageFormat(File.ReadAllBytes(filepath));

                file.FileName = filepath.Split('\\').Last();

                if (file.Extension != null) {
                    source.Add(file);
                }
            }

            var countComplete = source.Count;

            var returnValue = new List<Wallpaper>();

            switch (_imageFormat)
            {
                case pictureFormat.landscape:
                    returnValue = source
                                    .Where(p => Image.FromFile(p.Path).Width > Image.FromFile(p.Path).Height 
                                             && Image.FromFile(p.Path).Width == _imageWidth)
                                    .ToList();
                    Console.WriteLine("Importiere nur Querformat-Wallpaper ({0}).", returnValue.Count);
                    return returnValue;

                case pictureFormat.portrait:
                    returnValue = source
                                    .Where(p => Image.FromFile(p.Path).Width < Image.FromFile(p.Path).Height
                                             && Image.FromFile(p.Path).Height == _imageHeight)
                                    .ToList();
                    Console.WriteLine("Importiere nur Hochformat-Wallpaper ({0}).", returnValue.Count);
                    return returnValue;

                case pictureFormat.any:
                    returnValue = source
                                    .Where(p => Image.FromFile(p.Path).Height == _imageHeight || Image.FromFile(p.Path).Height == _imageWidth)
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
                    var md5hash = md5.ComputeHash(stream);

                    return BitConverter.ToString(md5hash).Replace("-", "").ToLower();
                }
            }
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
