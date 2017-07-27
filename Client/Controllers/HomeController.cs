using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            GetAuthTokenFromApi().Wait();
            return View();
        }

        private async Task GetAuthTokenFromApi()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55075");
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("username", "TEST"),
                new KeyValuePair<string, string>("password", "TEST123")
            });
                var result = await client.PostAsync("/token", content);
                string resultContent = await result.Content.ReadAsStringAsync();
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
