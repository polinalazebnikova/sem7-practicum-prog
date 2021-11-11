using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Requests
{
    public class BaseRequest
    {

        internal string PrefixTable;
        public BaseRequest(HttpRequest request)
        {
            foreach (var hdr in request.Headers.ToList())
            {
                switch (hdr.Key)
                {
                    case "prefix_table":
                        this.PrefixTable = hdr.Value.ToArray()[0].Trim();
                        break;
                }
            }
        }
    }
}
