namespace Wallpaper10CnC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Wallpaper
    {
        public Wallpaper(string path)
            : this(path, string.Empty)
        {
        }

        public Wallpaper(string path, string file)
            : this(path, file, string.Empty)
        {
        }

        public Wallpaper(string path, string file, string hash) 
            : this(path, file, hash, null)
        {
        }

        public Wallpaper(string path, string file, string hash, string extension)
        {
            this.Path = path;
            this.FileName = file;
            this.HashCode = hash;
            this.Extension = extension;
        }

        public string Path { get; set; }

        public string HashCode { get; set; }

        public string FileName { get; set; }

        public string Extension { get; set; }

        public ImageFormat GetImageFormat(byte[] bytes)
        {
            var formate = new List<Tuple<ImageFormat, byte[]>>
                              {
                                  new Tuple<ImageFormat, byte[]>(ImageFormat.BMP, Encoding.ASCII.GetBytes("BM")),
                                  new Tuple<ImageFormat, byte[]>(ImageFormat.GIF, Encoding.ASCII.GetBytes("GIF")),
                                  new Tuple<ImageFormat, byte[]>(ImageFormat.PNG, new byte[] { 137, 80, 78, 71 }),
                                  new Tuple<ImageFormat, byte[]>(ImageFormat.TIFF, new byte[] { 73, 73, 42 }),
                                  new Tuple<ImageFormat, byte[]>(ImageFormat.TIFF, new byte[] { 77, 77, 42 }),
                                  new Tuple<ImageFormat, byte[]>(ImageFormat.JEPG, new byte[] { 255, 216, 255, 224 }),
                                  new Tuple<ImageFormat, byte[]>(ImageFormat.JEPG, new byte[] { 255, 216, 255, 225 })
                              };

            foreach (var format in formate)
            {
                if (!format.Item2.SequenceEqual(bytes.Take(format.Item2.Length)))
                {
                    continue;
                }

                this.Extension = format.Item1.GetDescription();
                return format.Item1;
            }

            return ImageFormat.Unknown;
        }
    }
}
