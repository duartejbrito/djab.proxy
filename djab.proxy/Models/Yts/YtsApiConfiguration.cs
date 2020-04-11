using djab.proxy.Models.Interfaces;

namespace djab.proxy.Models.Yts
{
    public class YtsApiConfiguration : IApiConfiguration
    {
        public string BaseUrl { get; set; }
        public bool ByPass { get; set; }
    }
}
