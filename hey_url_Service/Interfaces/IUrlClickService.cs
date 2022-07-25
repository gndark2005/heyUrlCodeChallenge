using hey_url_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace hey_url_Service.Interfaces
{
    public interface IUrlClickService
    {
        public void Add(UrlClick urlClick);

        public Dictionary<string, int> GetDailyClicks(int Urlid);

        public Dictionary<string, int> GetReportBrowsers(int Urlid);

        public Dictionary<string, int> GetReportPlatform(int Urlid);

        public Dictionary<string, int> GetReportPlatformBrowsers(int Urlid);

    }
}
