using System.Collections.Generic;

namespace djab.proxy.Models.EzTv
{
    public class EzTvResponse
    {
        public int torrents_count { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public IEnumerable<EzTvTorrent> torrents { get; set; }
    }
}
