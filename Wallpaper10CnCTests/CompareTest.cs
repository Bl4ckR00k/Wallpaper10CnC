namespace Wallpaper10CnCTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using System.Linq;

    using Wallpaper10CnC;
    using Wallpaper10CnC.classes;

    [TestClass]
    public class CompareTest
    {
        private static readonly string ProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.FullName;
        private readonly string _source = ProjectPath + @"\Testfiles\compareTests\source";
        private readonly string _target = ProjectPath + @"\Testfiles\compareTests\target";

        [TestMethod]
        public void Compare_landscape_success()
        {
            WallpaperManager.Initialize(PictureFormat.Landscape, 1920, 1080);
            
            var pictures = WallpaperManager.GetSourcePictures(_source).ToList() ;

            Assert.AreEqual(2, pictures.Count, "Die Anzahl der geholten Bilder ist unerwartet abweichend");

            var result = WallpaperManager.Compare(pictures, _target).ToList();

            Assert.AreEqual(1, result.Count, "Die Anzahl der zu kopierenden Bilder unerwartet abweichend");
        }

        [TestMethod]
        public void Compare_portrait_success()
        {
            WallpaperManager.Initialize(PictureFormat.Portrait, 1080, 1920);

            var pictures = WallpaperManager.GetSourcePictures(_source).ToList();

            Assert.AreEqual(1, pictures.Count, "Die Anzahl der geholten Bilder ist unerwartet abweichend");

            var result = WallpaperManager.Compare(pictures, _target).ToList();

            Assert.AreEqual(1, result.Count, "Die Anzahl der zu kopierenden Bilder unerwartet abweichend");
        }

        [TestMethod]
        public void Compare_any_success()
        {
            WallpaperManager.Initialize(PictureFormat.Any, 1920, 1080);

            var pictures = WallpaperManager.GetSourcePictures(_source).ToList();

            Assert.AreEqual(3, pictures.Count, "Die Anzahl der geholten Bilder ist unerwartet abweichend");

            var result = WallpaperManager.Compare(pictures, _target).ToList();

            Assert.AreEqual(2, result.Count, "Die Anzahl der zu kopierenden Bilder unerwartet abweichend");
        }

        [TestMethod]
        public void Compare_none_success()
        {
            WallpaperManager.Initialize(PictureFormat.None, 0, 0);

            var pictures = WallpaperManager.GetSourcePictures(_source).ToList();

            Assert.AreEqual(4, pictures.Count, "Die Anzahl der geholten Bilder ist unerwartet abweichend");

            var result = WallpaperManager.Compare(pictures, _target).ToList();

            Assert.AreEqual(3, result.Count, "Die Anzahl der zu kopierenden Bilder unerwartet abweichend");
        }
    }
}
