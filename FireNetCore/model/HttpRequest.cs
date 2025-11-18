using System;
using System.Net.Http;

namespace FireNetCSharp.Model
{
    /// <summary>
    /// Http request model for 
    /// firewall api server
    /// </summary>
    public class HttpRequest : HttpClient
    {
        public HttpRequest()
        {
            this.Timeout = TimeSpan.FromSeconds(30);
        }
    }
}
