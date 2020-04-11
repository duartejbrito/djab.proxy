using System.Collections.Generic;

namespace djab.proxy.Models.Yts
{
    public class YtsMovies
    {
        public int movie_count { get; set; }
        public int limit { get; set; }
        public int page_number { get; set; }
        public IEnumerable<YtsMovie> movies { get; set; }

        public YtsMovies()
        {
            movies = new List<YtsMovie>();
        }
    }
}
