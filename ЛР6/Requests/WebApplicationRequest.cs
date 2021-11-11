using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Requests
{
    public class WebApplicationRequest
    {
        public double A { get; set; } = 0.0;
        public double H { get; set; } = 0.0;
        public string Guid { get; internal set; }

        public WebApplicationRequest(HttpRequest request)
        {
            var form = request.Form.ToList();

            foreach (var param in form)
            {
                switch (param.Key.ToLower())
                {
                    case "a":
                        this.A = Convert.ToDouble(param.Value.ToArray()[0].ToString());
                        break;
                    case "h":
                        this.H = Convert.ToDouble(param.Value.ToArray()[0].ToString());
                        break;
                    case "uuid":
                        this.Guid = Convert.ToString(param.Value.ToArray()[0].ToString());
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
