using hey_url_API.ViewModels;
using hey_url_Model;
using hey_url_Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_API.Controllers
{
    [ApiController]
    [Route("api/Url")]
    public class UrlApiController : Controller
    {
        public IUrlService urlService;
        public IUrlClickService urlClickService;

        public UrlApiController(IUrlService urlservice, IUrlClickService urlclickservice)
        {

            this.urlService = urlservice;
            this.urlClickService = urlclickservice;
        }

        [HttpGet]
        public IEnumerable<UrlForApi> Get() {

            List<UrlForApi> result= new List<UrlForApi>() ;
            foreach (Url record in this.urlService.GetAll(-1))
            {
                result.Add(new UrlForApi(record));
            }
            return result;
        }
         
    }
}
