﻿namespace djab.proxy.Models.Interfaces
{
    public interface IApiConfiguration
    {
        public string BaseUrl { get; set; }
        public bool ByPass { get; set; }
    }
}
