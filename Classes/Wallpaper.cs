namespace Wallpaper10CnC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Wallpaper
    {
        public string Path { get; set; }
        public string HashCode { get; set; }
        public string FileName { get; set; }

        public string Extension { get; set; }

        public Wallpaper(string path) : this(path, string.Empty) { }
        public Wallpaper(string path, string file) : this(path, file, string.Empty) { }
        public Wallpaper(string path, string file, string hash)
        {
            Path = path;
            FileName = file;
            HashCode = hash;
        }

        public ImageFormat GetImageFormat(byte[] bytes)
        {
            var formate = new List<Tuple<ImageFormat, byte[]>>();

            formate.Add(new Tuple<ImageFormat, byte[]>(ImageFormat.bmp, Encoding.ASCII.GetBytes("BM")));
            formate.Add(new Tuple<ImageFormat, byte[]>(ImageFormat.gif, Encoding.ASCII.GetBytes("GIF")));
            formate.Add(new Tuple<ImageFormat, byte[]>(ImageFormat.png, new byte[] { 137, 80, 78, 71 }));
            formate.Add(new Tuple<ImageFormat, byte[]>(ImageFormat.tiff, new byte[] { 73, 73, 42 }));
            formate.Add(new Tuple<ImageFormat, byte[]>(ImageFormat.tiff, new byte[] { 77, 77, 42 }));
            formate.Add(new Tuple<ImageFormat, byte[]>(ImageFormat.jpeg, new byte[] { 255, 216, 255, 224 }));
            formate.Add(new Tuple<ImageFormat, byte[]>(ImageFormat.jpeg, new byte[] { 255, 216, 255, 225 }));

            foreach (var format in formate)
            {
                if (format.Item2.SequenceEqual(bytes.Take(format.Item2.Length)))
                {
                    Extension = Helper.GetDescription(format.Item1);
                    return format.Item1;
                }
            }

            return ImageFormat.unknown;
        }
    }
}
