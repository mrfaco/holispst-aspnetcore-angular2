using Client.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers.Materias
{
    [Authorize]
    public class MateriasController : Controller
    {
        private Auth _token;
        private IConfigurationRoot _config;

        public MateriasController(IConfigurationRoot config)
        {
            this._config = config;
            GetAuthenticationToken().Wait();
        }

        private async Task GetAuthenticationToken()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_config["ApiConfig:ApiBaseAddress"]);
                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("username", "TEST"));
                nvc.Add(new KeyValuePair<string, string>("password", "TEST123"));

                var response = await httpClient.PostAsync("/token", new FormUrlEncodedContent(nvc));

                var data = await response.Content.ReadAsStringAsync();
                this._token = JsonConvert.DeserializeObject<Auth>(data);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Materias()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_config["ApiConfig:ApiBaseAddress"]);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.access_token);
                var response = httpClient.GetAsync(_config["ApiConfig:MateriasAddress"]);
                var json = await response.Result.Content.ReadAsStringAsync();
                return Ok(json);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<Materia> model)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_config["ApiConfig:ApiBaseAddress"]);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.access_token);
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(_config["ApiConfig:MateriasAddress"], content);
                var json = await response.Content.ReadAsStringAsync();
                return Ok(json);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<Materia> model)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_config["ApiConfig:ApiBaseAddress"]);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.access_token);
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(_config["ApiConfig:MateriasAddress"], content);
                var json = await response.Content.ReadAsStringAsync();
                return Ok(json);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_config["ApiConfig:ApiBaseAddress"]);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.access_token);
                var response = await httpClient.DeleteAsync(_config["ApiConfig:MateriasAddress"]+$"/{id}");
                var json = await response.Content.ReadAsStringAsync();
                return Ok(json);
            }
        }
    }
}

