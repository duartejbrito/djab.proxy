namespace djab.proxy.Models.Yts
{
    public class YtsResponse<T>
    {
        public string status { get; set; }
        public string status_message { get; set; }
        public T data { get; set; }

        public YtsResponse()
        {

        }
    }
}
