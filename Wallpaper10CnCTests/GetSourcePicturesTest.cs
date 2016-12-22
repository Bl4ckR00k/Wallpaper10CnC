namespace Wallpaper10CnC.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    using Wallpaper10CnC;

    [TestClass]
    public class GetSourcePicturesTest
    {
        [TestMethod]
        public void GetSourcePictures_landscape_success()
        {
            string path = @"C:\source";
            var wpm = new WallpaperManager(PictureFormat.Landscape, 1920, 1080);
            
            var pictures = wpm.GetSourcePictures(path).ToList() ;

            Assert.AreEqual(2, pictures.Count, "Die Anzahl ist unerwartet abweichend");         
        }

        [TestMethod]
        public void GetSourcePictures_portrait_success()
        {
            string path = @"C:\source";
            var wpm = new WallpaperManager(PictureFormat.Portrait, 1080, 1920);

            var pictures = wpm.GetSourcePictures(path).ToList();

            Assert.AreEqual(1, pictures.Count, "Die Anzahl ist unerwartet abweichend");
        }

        [TestMethod]
        public void GetSourcePictures_any_success()
        {
            string path = @"C:\source";
            var wpm = new WallpaperManager(PictureFormat.Any, 1920, 1080);

            var pictures = wpm.GetSourcePictures(path).ToList();

            Assert.AreEqual(3, pictures.Count, "Die Anzahl ist unerwartet abweichend");
        }

        [TestMethod]
        public void GetSourcePictures_none_success()
        {
            string path = @"C:\source";
            var wpm = new WallpaperManager(PictureFormat.None, 0, 0);

            var pictures = wpm.GetSourcePictures(path).ToList();

            Assert.AreEqual(4, pictures.Count, "Die Anzahl ist unerwartet abweichend");
        }

    }
}
