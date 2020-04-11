using djab.proxy.Models.Yts;
using System.Threading.Tasks;

namespace djab.proxy.Services
{
    public interface IYtsService
    {
        Task<YtsResponse<YtsMovies>> Get(int page, string quality = "720p");
    }
}
