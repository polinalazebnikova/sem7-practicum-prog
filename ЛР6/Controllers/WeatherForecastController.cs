using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Requests;
using WebApplication1.Scructures;
using WebApplication1.Services;

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

        [HttpPost]
        [HttpGet]
        [Produces("application/json")]
        [Route("create_calc")]
        public async Task<IActionResult> CreateCalc()
        {
            WebApplicationRequest request = new WebApplicationRequest(this.Request);

            string path = Path.Combine(Params.DataScr, "json_settings.txt");
            List<Results> content = new List<Results>();
            string str_content = System.IO.File.ReadAllText(path);
            if (!string.IsNullOrEmpty(str_content))
            {
                content = JsonConvert.DeserializeObject<List<Results>>(str_content);
            }
            Results elem = new Results();
            elem.uuid = Guid.NewGuid().ToString();
            elem.result = request.A * request.H;
            content.Add(elem);

            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(content));

            return this.Json(elem);
        }

        [HttpPost]
        [HttpGet]
        [Produces("application/json")]
        [Route("get_one_calc")]
        public async Task<IActionResult> GetOneCalc()
        {
            WebApplicationRequest request = new WebApplicationRequest(this.Request);

            string path = Path.Combine(Params.DataScr, "json_settings.txt");
            Results obj = null;
            string str_content = System.IO.File.ReadAllText(path);
            if (!string.IsNullOrEmpty(str_content))
            {
                List<Results> content = JsonConvert.DeserializeObject<List<Results>>(str_content);
                foreach (var c in content)
                {
                    if (c.uuid == request.Guid)
                    {
                        obj = c;
                    }
                }
            }

            return this.Json(obj);
        }

        [HttpPost]
        [HttpGet]
        [Produces("application/json")]
        [Route("update_one_calc")]
        public async Task<IActionResult> UpdateOneCalc()
        {
            WebApplicationRequest request = new WebApplicationRequest(this.Request);
            bool update = false;

            List<Results> content = new List<Results>();
            string path = Path.Combine(Params.DataScr, "json_settings.txt");
            string str_content = System.IO.File.ReadAllText(path);
            if (!string.IsNullOrEmpty(str_content))
            {
                content = JsonConvert.DeserializeObject<List<Results>>(str_content);
                foreach (var b in content)
                {
                    if (b.uuid == request.Guid)
                    {
                        b.result = request.A * request.H;
                        update = true;
                    }
                }
            }
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(content));

            return this.Json(new
            {
                result = update
            });
        }

        [HttpPost]
        [HttpGet]
        [Produces("application/json")]
        [Route("delete_one_calc")]
        public async Task<IActionResult> DeleteOneCalc()
        {
            WebApplicationRequest request = new WebApplicationRequest(this.Request);
            bool del = false;

            List<Results> content = new List<Results>();
            List<Results> final = new List<Results>();
            string path = Path.Combine(Params.DataScr, "json_settings.txt");
            string str_content = System.IO.File.ReadAllText(path);
            if (!string.IsNullOrEmpty(str_content))
            {
                content = JsonConvert.DeserializeObject<List<Results>>(str_content);
                foreach (var b in content)
                {
                    if (b.uuid != request.Guid)
                    {
                        final.Add(b);
                        del = true;
                    }
                }
            }
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(content));

            return this.Json(new
            {
                result = del
            });
        }

    }
}
