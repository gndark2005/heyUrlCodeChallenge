using hey_url_Model;
using hey_url_Service.Interfaces;
using HeyUrlChallengeCodeDotnet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hey_url_Service.Implementations
{
    public class UrlClickService : IUrlClickService
    {

        public ApplicationContext DBContext;

        public UrlClickService()
        {
        }
        public UrlClickService(ApplicationContext Context)
        {
            this.DBContext = Context;
        }
        public void Add(UrlClick urlClick)
        {
            using (var tr = DBContext.Database.BeginTransaction())
            {
                try
                {
                    urlClick.ClickDateTime = DateTime.Now;
                    DBContext.UrlClick.Add(urlClick);
                    DBContext.SaveChanges();
                    urlClick.Url.Count += 1;
                    DBContext.Url.Update(urlClick.Url);
                    DBContext.SaveChanges();
                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    throw new Exception("Error when saving data : " + ex.Message);
                }
            }
        }

        public Dictionary<string, int> GetDailyClicks(int urlid)
        {
            return (DBContext.UrlClick.Where(w => w.UrlId == urlid)
                     .GroupBy(g => new { day = g.ClickDateTime.Day.ToString() })
                     .Select(s => new { key = s.Key.day, value = s.Count() }).ToDictionary(k => k.key, v => v.value));
        }

        public Dictionary<string, int> GetReportBrowsers(int urlid)
        {
            return (DBContext.UrlClick.Where(w => w.UrlId == urlid)
                     .GroupBy(g => new { brower = g.Browser })
                     .Select(s => new { key = s.Key.brower, value = s.Count() }).ToDictionary(k => k.key, v => v.value));
        }

        public Dictionary<string, int> GetReportPlatform(int urlid)
        {
            return (DBContext.UrlClick.Where(w => w.UrlId == urlid)
                     .GroupBy(g => new { Platform = g.Platform })
                     .Select(s => new { key = s.Key.Platform, value = s.Count() }).ToDictionary(k => k.key, v => v.value));
        }

        public Dictionary<string, int> GetReportPlatformBrowsers(int urlid)
        {
            return (DBContext.UrlClick.Where(w => w.UrlId == urlid)
                        .GroupBy(g => new {  g.Platform , g.Browser} )
                        .Select(s => new { key = string.Format("{0}{1}{2}",s.Key.Platform,"-",s.Key.Browser), value = s.Count() }).ToDictionary(k => k.key, v => v.value));
        }
    }
}
