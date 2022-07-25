using System.Collections.Generic;
using hey_url_Model;

namespace hey_url_challenge_code_dotnet.ViewModels
{
    public class ShowViewModel
    {
        public Url Url { get; set; }
        public Dictionary<string, int> DailyClicks { get; set; }
        public Dictionary<string, int> BrowseClicks { get; set; }
        public Dictionary<string, int> PlatformClicks { get; set; }
        public Dictionary<string, int> BrowsePlatformClicks { get; set; }
    }
}