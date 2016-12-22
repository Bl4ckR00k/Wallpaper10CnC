namespace Wallpaper10CnC.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using System.Linq;

    using Wallpaper10CnC;

    [TestClass]
    public class CompareTest
    {
        private static string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        private string Source = projectPath + @"\Testfiles\compareTests\source";
        private string Target = projectPath + @"\Testfiles\compareTests\target";

        [TestMethod]
        public void Compare_landscape_success()
        {
            var wpm = new WallpaperManager(PictureFormat.Landscape, 1920, 1080);
            
            var pictures = wpm.GetSourcePictures(Source).ToList() ;

            Assert.AreEqual(2, pictures.Count, "Die Anzahl der geholten Bilder ist unerwartet abweichend");

            var result = wpm.Compare(pictures, Target).ToList();

            Assert.AreEqual(1, result.Count, "Die Anzahl der zu kopierenden Bilder unerwartet abweichend");
        }

        [TestMethod]
        public void Compare_portrait_success()
        {
            var wpm = new WallpaperManager(PictureFormat.Portrait, 1080, 1920);

            var pictures = wpm.GetSourcePictures(Source).ToList();

            Assert.AreEqual(1, pictures.Count, "Die Anzahl der geholten Bilder ist unerwartet abweichend");

            var result = wpm.Compare(pictures, Target).ToList();

            Assert.AreEqual(1, result.Count, "Die Anzahl der zu kopierenden Bilder unerwartet abweichend");
        }

        [TestMethod]
        public void Compare_any_success()
        {
            var wpm = new WallpaperManager(PictureFormat.Any, 1920, 1080);

            var pictures = wpm.GetSourcePictures(Source).ToList();

            Assert.AreEqual(3, pictures.Count, "Die Anzahl der geholten Bilder ist unerwartet abweichend");

            var result = wpm.Compare(pictures, Target).ToList();

            Assert.AreEqual(2, result.Count, "Die Anzahl der zu kopierenden Bilder unerwartet abweichend");
        }

        [TestMethod]
        public void Compare_none_success()
        {
            var wpm = new WallpaperManager(PictureFormat.None, 0, 0);

            var pictures = wpm.GetSourcePictures(Source).ToList();

            Assert.AreEqual(4, pictures.Count, "Die Anzahl der geholten Bilder ist unerwartet abweichend");

            var result = wpm.Compare(pictures, Target).ToList();

            Assert.AreEqual(3, result.Count, "Die Anzahl der zu kopierenden Bilder unerwartet abweichend");
        }
    }
}
