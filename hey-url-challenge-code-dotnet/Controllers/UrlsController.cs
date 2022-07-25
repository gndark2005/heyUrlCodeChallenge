using System;
using System.Collections.Generic;
using hey_url_Model;
using hey_url_Service;
using hey_url_challenge_code_dotnet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;
using hey_url_Service.Interfaces;
using hey_url_challenge_code_dotnet.Helper;

namespace HeyUrlChallengeCodeDotnet.Controllers
{
    [Route("/")]
    public class UrlsController : Controller
    {
        private readonly ILogger<UrlsController> _logger;
        private static readonly Random getrandom = new Random();
        private readonly IBrowserDetector browserDetector;
        public IUrlService urlService;
        public IUrlClickService urlClickService;

        public UrlsController(ILogger<UrlsController> logger, IBrowserDetector browserDetector, IUrlService urlservice, IUrlClickService urlclickservice)
        {
            this.browserDetector = browserDetector;
            this.urlService = urlservice;
            this.urlClickService = urlclickservice;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var model = new HomeViewModel();
                model.Urls = urlService.GetAll(-1);
                model.NewUrl = new();
                ViewBag.baseurl = GlobalValues.BaseUrl;
                return View(model);
            }
            catch (Exception ex)
            {
                //log exception
                return BadRequest();
            }
        }

        [Route("/{url}")]
        public IActionResult Visit(string url)
        {
            try
            {
                Url UrlVisited = this.urlService.Get(url);
                if (UrlVisited == null)
                {
                    return new NotFoundResult();
                }
                UrlClick newrecord = new UrlClick();
                newrecord.Url = UrlVisited;
                newrecord.UrlId = UrlVisited.UrlId;
                newrecord.Browser = this.browserDetector.Browser.Name;
                newrecord.Platform = this.browserDetector.Browser.OS;
                this.urlClickService.Add(newrecord);
                return Redirect(UrlVisited.OrinalUrl);
            }
            catch (Exception ex)
            {
                //log exception
                return new BadRequestResult();
            }
        }

        [Route("urls/{urlid}")]
        public IActionResult Show(int urlid)
        {
            try
            {
                ShowViewModel model = new ShowViewModel();
                model.Url = this.urlService.Get(urlid);
                model.BrowseClicks = this.urlClickService.GetReportBrowsers(urlid);
                model.PlatformClicks = this.urlClickService.GetReportPlatform(urlid);
                model.BrowsePlatformClicks = this.urlClickService.GetReportPlatformBrowsers(urlid);
                model.DailyClicks = this.urlClickService.GetDailyClicks(urlid);
                return View("Show", model);
            }
            catch (Exception ex)
            {
                //log exception
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public IActionResult Create(HomeViewModel model)
        {
            try
            {
                model.NewUrl.ShortUrl = "";
                model.NewUrl.CretionDate = DateTime.Now;
                model.NewUrl.Count = 0;
                this.urlService.Add(model.NewUrl);
                model.NewUrl.ShortUrl = GlobalValues.BaseUrl +   model.NewUrl.ShortUrl;
                return PartialView("create", model.NewUrl);
            }
            catch
            {
                //log exception
                return new BadRequestResult();
            }
        }

    }
}