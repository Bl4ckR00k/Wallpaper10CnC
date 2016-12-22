namespace Wallpaper10CnC.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;

    using Wallpaper10CnC;

    [TestClass]
    public class TransferTest
    {
        [TestMethod]
        public void Transfer_landscape_success()
        {
            string source = @"C:\source";
            string target = @"C:\target";
            var wpm = new WallpaperManager(PictureFormat.Landscape, 1920, 1080);
            
            var pictures = wpm.GetSourcePictures(source).ToList() ;

            Assert.AreEqual(2, pictures.Count, "Anzahl ermittelter Bilder");

            var result = wpm.Compare(pictures, target).ToList();

            Assert.AreEqual(1, result.Count, "Anzahl vergleichener Bilder");

            string transferTarget = @"C:\target\" + DateTime.Now.ToString("yyyyMMddHHmmss") +  @"\";

            Directory.CreateDirectory(transferTarget);
            try
            {
                wpm.Transfer(result, transferTarget);

                var transfers = Directory.GetFiles(transferTarget);

                Assert.AreEqual(1, transfers.Count(), "Anzahl der Transfers");
            }
            finally
            {
                Directory.Delete(transferTarget, true);
            }
        }
    }
}
