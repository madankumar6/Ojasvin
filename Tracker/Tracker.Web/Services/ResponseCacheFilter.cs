using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
