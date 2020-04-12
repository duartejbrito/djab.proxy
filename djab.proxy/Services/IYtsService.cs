using djab.proxy.Models.Yts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace djab.proxy.Services
{
    public interface IYtsService
    {
        Task<YtsResponse<YtsMovies>> Get(int page, YtsQuality quality = YtsQuality.Low);
        Task<FileStreamResult> Torrent(string torrent);
    }
}
