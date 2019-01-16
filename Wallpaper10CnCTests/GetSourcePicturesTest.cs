namespace Wallpaper10CnCTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using System.Linq;

    using Wallpaper10CnC;
    using Wallpaper10CnC.classes;

    [TestClass]
    public class GetSourcePicturesTest
    {
        private static readonly string ProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.FullName;
        private readonly string _path = ProjectPath + @"\Testfiles\getsourcepicturesTest\source";
        private const string ErrorMessage = @"Die Anzahl ist unerwartet abweichend.";

        [TestMethod]
        public void GetSourcePictures_landscape_success()
        {
            WallpaperManager.Initialize(PictureFormat.Landscape, 1920, 1080);
            
            var pictures = WallpaperManager.GetSourcePictures(_path).ToList() ;

            Assert.AreEqual(2, pictures.Count, ErrorMessage);         
        }

        [TestMethod]
        public void GetSourcePictures_portrait_success()
        {
            WallpaperManager.Initialize(PictureFormat.Portrait, 1080, 1920);

            var pictures = WallpaperManager.GetSourcePictures(_path).ToList();

            Assert.AreEqual(1, pictures.Count, ErrorMessage);
        }

        [TestMethod]
        public void GetSourcePictures_any_success()
        {
            WallpaperManager.Initialize(PictureFormat.Any, 1920, 1080);

            var pictures = WallpaperManager.GetSourcePictures(_path).ToList();

            Assert.AreEqual(3, pictures.Count, ErrorMessage);
        }

        [TestMethod]
        public void GetSourcePictures_none_success()
        {
            WallpaperManager.Initialize(PictureFormat.None, 0, 0);

            var pictures = WallpaperManager.GetSourcePictures(_path).ToList();

            Assert.AreEqual(4, pictures.Count, ErrorMessage);
        }

    }
}
