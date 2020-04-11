using djab.proxy.Models.EzTv;
using System.Threading.Tasks;

namespace djab.proxy.Services
{
    public interface IEzTvService
    {
        Task<EzTvResponse> Get(int page = 1, int limit = 100);
    }
}