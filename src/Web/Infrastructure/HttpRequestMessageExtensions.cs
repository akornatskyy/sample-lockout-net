using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Web.Infrastructure
{
    public static class HttpRequestMessageExtensions
    {
        public static string ClientIpAddress(this HttpRequestMessage req)
        {
            var ip = TryGetLastHttpHeaderValue(req, "X-Forwarded-For");
            if (!string.IsNullOrEmpty(ip))
            {
                return ip;
            }

            if (req.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)req.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            
            return HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : null;
        }

        private static string TryGetLastHttpHeaderValue(HttpRequestMessage request, string headerName)
        {
            IEnumerable<string> values;
            if (!request.Headers.TryGetValues(headerName, out values))
            {
                return null;
            }

            var value = values.LastOrDefault();
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return value.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
        }
    }
}