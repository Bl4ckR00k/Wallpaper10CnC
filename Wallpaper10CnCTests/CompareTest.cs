namespace Wallpaper10CnC.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    using Wallpaper10CnC;

    [TestClass]
    public class CompareTest
    {
        [TestMethod]
        public void Compare_landscape_success()
        {
            string source = @"C:\source";
            string target = @"C:\target";
            var wpm = new WallpaperManager(PictureFormat.Landscape, 1920, 1080);
            
            var pictures = wpm.GetSourcePictures(source).ToList() ;

            Assert.AreEqual(2, pictures.Count, "Die Anzahl der geholten Bilder ist unerwartet abweichend");

            var result = wpm.Compare(pictures, target).ToList();

            Assert.AreEqual(1, result.Count, "Die Anzahl der zu kopierenden Bilder unerwartet abweichend");
        }

        [TestMethod]
        public void Compare_portrait_success()
        {
            string source = @"C:\source";
            string target = @"C:\target";
            var wpm = new WallpaperManager(PictureFormat.Portrait, 1080, 1920);

            var pictures = wpm.GetSourcePictures(source).ToList();

            Assert.AreEqual(1, pictures.Count, "Die Anzahl der geholten Bilder ist unerwartet abweichend");

            var result = wpm.Compare(pictures, target).ToList();

            Assert.AreEqual(1, result.Count, "Die Anzahl der zu kopierenden Bilder unerwartet abweichend");
        }

        [TestMethod]
        public void Compare_any_success()
        {
            string source = @"C:\source";
            string target = @"C:\target";
            var wpm = new WallpaperManager(PictureFormat.Any, 1920, 1080);

            var pictures = wpm.GetSourcePictures(source).ToList();

            Assert.AreEqual(3, pictures.Count, "Die Anzahl der geholten Bilder ist unerwartet abweichend");

            var result = wpm.Compare(pictures, target).ToList();

            Assert.AreEqual(2, result.Count, "Die Anzahl der zu kopierenden Bilder unerwartet abweichend");
        }

        [TestMethod]
        public void Compare_none_success()
        {
            string source = @"C:\source";
            string target = @"C:\target";
            var wpm = new WallpaperManager(PictureFormat.None, 0, 0);

            var pictures = wpm.GetSourcePictures(source).ToList();

            Assert.AreEqual(4, pictures.Count, "Die Anzahl der geholten Bilder ist unerwartet abweichend");

            var result = wpm.Compare(pictures, target).ToList();

            Assert.AreEqual(3, result.Count, "Die Anzahl der zu kopierenden Bilder unerwartet abweichend");
        }
    }
}
