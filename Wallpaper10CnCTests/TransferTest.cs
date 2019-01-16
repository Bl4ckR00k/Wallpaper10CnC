namespace Wallpaper10CnCTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;

    using Wallpaper10CnC;
    using Wallpaper10CnC.classes;

    [TestClass]
    public class TransferTest
    {
        private static readonly string ProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.FullName;
        private readonly string _source = ProjectPath + @"\Testfiles\transferTests\source";
        private readonly string _target = ProjectPath + @"\Testfiles\transferTests\target";

        [TestMethod]
        public void Transfer_landscape_success()
        {
            WallpaperManager.Initialize(PictureFormat.Landscape, 1920, 1080);
            
            var pictures = WallpaperManager.GetSourcePictures(_source).ToList() ;

            Assert.AreEqual(2, pictures.Count, "Anzahl ermittelter Bilder");

            var result = WallpaperManager.Compare(pictures, _target).ToList();

            Assert.AreEqual(1, result.Count, "Anzahl vergleichener Bilder");

            var transferTarget = _target + @"\" + DateTime.Now.ToString("yyyyMMddHHmmss") +  @"\";

            Directory.CreateDirectory(transferTarget);
            try
            {
                WallpaperManager.CopyWallpapersToTarget(result, transferTarget);

                var transfers = Directory.GetFiles(transferTarget);

                Assert.AreEqual(1, transfers.Length, "Anzahl der Transfers");
            }
            finally
            {
                Directory.Delete(transferTarget, true);
            }
        }
    }
}
