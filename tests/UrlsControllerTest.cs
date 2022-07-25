using NUnit.Framework;
using hey_url_Service;
using hey_url_Service.Helpers;
using Moq;
using hey_url_Service.Interfaces;
using HeyUrlChallengeCodeDotnet.Controllers;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;
using hey_url_Model;
using Microsoft.AspNetCore.Mvc;
using hey_url_challenge_code_dotnet.ViewModels;

namespace tests
{
    public class UrlsControllerTest
    {
        private UrlsController  _UrlControler ;

        [SetUp]
        public void Setup()
        {
            Mock<IUrlService> MockUrlService = new Mock<IUrlService>();
            Mock<IUrlClickService> MockUrlClickService = new Mock<IUrlClickService>();
            Mock<ILogger<UrlsController>> MockUpLogger = new Mock<ILogger<UrlsController>>();
            Mock<IBrowserDetector> MockBrowserDetector = new Mock<IBrowserDetector>();

            MockUrlService.Setup(s => s.Get("QQQQQ")).Returns(new Url() { UrlId = 1, Count = 1, CretionDate = System.DateTime.Now, ShortUrl = "QQQQQ", OrinalUrl = "HTTPS://WWW.GOOGLE.COM" });
            MockUrlClickService.Setup(s => s.Add(It.IsAny<UrlClick>())).Verifiable();
            MockBrowserDetector.Setup(s => s.Browser.Name).Returns("Nunit");
            MockBrowserDetector.Setup(s => s.Browser.OS).Returns("Windows");
            MockBrowserDetector.Setup(s => s.Browser.DeviceType).Returns("Laptop");
            MockBrowserDetector.Setup(s => s.Browser.Version).Returns("1.1");


            this._UrlControler = new UrlsController(MockUpLogger.Object, MockBrowserDetector.Object, MockUrlService.Object, MockUrlClickService.Object);

        }

        [Test]
        public void TestIndex()
        {
            IActionResult result = this._UrlControler.Index();
            Assert.AreNotEqual(result.GetType(), typeof(BadRequestResult));
        }
        [Test]
        public void Visit()
        {
            //200
            IActionResult result = this._UrlControler.Visit("QQQQQ");
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.GetType(), typeof(BadRequestResult));

            //404
            result = this._UrlControler.Visit("XXX");
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
        }

        [Test]
        public void Show()
        {
            IActionResult result = this._UrlControler.Show(2);
            Assert.AreNotEqual(result.GetType(), typeof(BadRequestResult));
        }
        [Test]
        public void Create()
        {
            HomeViewModel model= new HomeViewModel();
            model.NewUrl = new Url() { UrlId = 2, ShortUrl = "XXXXX", Count = 10, CretionDate = System.DateTime.Now, OrinalUrl = "https://anything.com" };
            IActionResult result = this._UrlControler.Create(model);
            Assert.AreNotEqual(result.GetType(), typeof(BadRequestResult));
        }

        [Test]
        public void TestHash()
        {
            string result = HashCalculator.CalculateHash(1);
            Assert.AreEqual("QQQQH", result);
        }

    }
}