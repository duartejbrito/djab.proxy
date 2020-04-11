using System.Collections.Generic;

namespace djab.proxy.Models.Yts
{
    public class YtsMovie
    {
        public string url { get; set; }
        public string title { get; set; }
        public IEnumerable<YtsTorrent> torrents { get; set; }
        public string date_uploaded { get; set; }

        public YtsMovie()
        {

        }
    }
}
