using System.ComponentModel;

namespace djab.proxy.Models.Yts
{
    public enum YtsQuality
    {
        [Description("720p")]
        Low,
        [Description("1080p")]
        High,
        [Description("3D")]
        TreeD
    }
}
