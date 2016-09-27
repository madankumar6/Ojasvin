using Microsoft.AspNetCore.Mvc;

namespace Tracker.Web.Services
{
    public class ResponseCacheFilter : ResponseCacheAttribute
    {
        public ResponseCacheFilter() : base()
        {
            Location = ResponseCacheLocation.None;
            NoStore = true;
        }
    }
}
