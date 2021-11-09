using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Requests;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("test")]
    public class WeatherForecastController : Controller
    {
        [HttpPost]
        [HttpGet]
        [Produces("application/json")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            return this.Json(new { result = "Hello World!" });
        }

        [HttpPost]
        [HttpGet]
        [Produces("application/json")]
        [Route("square")]
        public async Task<IActionResult> Square()
        {
            WebApplicationRequest request = new WebApplicationRequest(this.Request);

            double sq = request.A * request.H;

            MyClassResponse res = new MyClassResponse();
            res.Success = "success";
            res.Result = sq;
            res.Version = "1.0";
            return this.Json(res);
        }
    }
}
