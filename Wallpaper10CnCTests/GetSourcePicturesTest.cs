namespace Wallpaper10CnC.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using System.Linq;

    using Wallpaper10CnC;

    [TestClass]
    public class GetSourcePicturesTest
    {
        private static string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        private string Path = projectPath + @"\Testfiles\getsourcepicturesTest\source";
        private const string ErrorMessage = @"Die Anzahl ist unerwartet abweichend.";

        [TestMethod]
        public void GetSourcePictures_landscape_success()
        {
            var wpm = new WallpaperManager(PictureFormat.Landscape, 1920, 1080);
            
            var pictures = wpm.GetSourcePictures(Path).ToList() ;

            Assert.AreEqual(2, pictures.Count, ErrorMessage);         
        }

        [TestMethod]
        public void GetSourcePictures_portrait_success()
        {
            var wpm = new WallpaperManager(PictureFormat.Portrait, 1080, 1920);

            var pictures = wpm.GetSourcePictures(Path).ToList();

            Assert.AreEqual(1, pictures.Count, ErrorMessage);
        }

        [TestMethod]
        public void GetSourcePictures_any_success()
        {
            var wpm = new WallpaperManager(PictureFormat.Any, 1920, 1080);

            var pictures = wpm.GetSourcePictures(Path).ToList();

            Assert.AreEqual(3, pictures.Count, ErrorMessage);
        }

        [TestMethod]
        public void GetSourcePictures_none_success()
        {
            var wpm = new WallpaperManager(PictureFormat.None, 0, 0);

            var pictures = wpm.GetSourcePictures(Path).ToList();

            Assert.AreEqual(4, pictures.Count, ErrorMessage);
        }

    }
}
