using Client.UsersContext;
using Client.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers.Materias
{
    public class MateriasController : Controller
    {
        private IConfigurationRoot _config;
        private IMemoryCache _memoryCache;

        public MateriasController(IConfigurationRoot config, IMemoryCache memoryCache)
        {
            this._config = config;
            this._memoryCache = memoryCache;
        }
        
        [HttpGet]
        public async Task<IActionResult> Materias()
        {
            var cached = string.Empty;
            _memoryCache.TryGetValue("token", out cached);
            if (!CheckIfIsLocal() || cached == string.Empty)
            {
                return BadRequest();
            }
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_config["ApiConfig:ApiBaseAddress"]);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",cached);
                var response = httpClient.GetAsync(_config["ApiConfig:MateriasAddress"]);
                var json = await response.Result.Content.ReadAsStringAsync();
                return Ok(json);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<Materia> model)
        {
            var cached = string.Empty;
            _memoryCache.TryGetValue("token", out cached);
            if (!CheckIfIsLocal() || cached == string.Empty)
            {
                return BadRequest();
            }
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_config["ApiConfig:ApiBaseAddress"]);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cached);
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(_config["ApiConfig:MateriasAddress"], content);
                var json = await response.Content.ReadAsStringAsync();
                return Ok(json);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<Materia> model)
        {
            var cached = string.Empty;
            _memoryCache.TryGetValue("token", out cached);
            if (!CheckIfIsLocal() || cached == string.Empty)
            {
                return BadRequest();
            }
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_config["ApiConfig:ApiBaseAddress"]);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cached);
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(_config["ApiConfig:MateriasAddress"], content);
                var json = await response.Content.ReadAsStringAsync();
                return Ok(json);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var cached = string.Empty;
            _memoryCache.TryGetValue("token", out cached);
            if (!CheckIfIsLocal() || cached == string.Empty)
            {
                return BadRequest();
            }
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_config["ApiConfig:ApiBaseAddress"]);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cached);
                var response = await httpClient.DeleteAsync(_config["ApiConfig:MateriasAddress"] + string.Format($"/{id}"));
                var json = await response.Content.ReadAsStringAsync();
                return Ok(json);
            }
        }
        private bool CheckIfIsAuthenticated()
        {
            var cached = string.Empty;
            _memoryCache.TryGetValue("token", out cached);
            if (cached != string.Empty)
            {
                return true;
            }
            return false;
        }

        private bool CheckIfIsLocal()
        {
            if (Request.IsLocal())
            {
                return true;
            }
            return false;
        }
    }
}

