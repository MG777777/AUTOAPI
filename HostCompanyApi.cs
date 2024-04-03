using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AUTOAPI
{
    public class HostCompanyApi
    {
        private HttpClient _httpClient = new HttpClient();
        private ExecutionContext _context;
        private ILogger _log;
        private TokenService _tokenService { get; set; }

        private ApiConfiguration _configuration;
        public HostCompanyApi(ILogger logger, ExecutionContext context)
        {
            _httpClient = new HttpClient();
            _context = context;
            _log = logger;
            _tokenService = new TokenService(_log, _context);
            string resourceName = "configHostCompany.json";
            string jsonConfig = File.ReadAllText(Path.Combine(_context.FunctionAppDirectory, resourceName));
            if (String.IsNullOrEmpty(jsonConfig))
            {
                throw new IOException();
               
            }
            _configuration = JsonSerializer.Deserialize<ApiConfiguration>(jsonConfig) ?? new ApiConfiguration();

        }
        public async Task<List<T>> GetAllAsync<T>(string url)
        {
            TokenInfo _token = await _tokenService.GetNewToken();
           
           

            using (HttpClient apiClient = new HttpClient())
            {
                if(_configuration.ApiUrl is null)
                       return new List<T>();
                apiClient.BaseAddress = new Uri(_configuration.ApiUrl);

                apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token!.access_token);
                HttpResponseMessage response = await apiClient.GetAsync(url);
                if (response is not null && response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<T>>(responseContent);

                    return result is null ? new List<T>() : result;
                }
                else
                {
                    _log.LogWarning($"Error when getting {typeof(T).Name}, http status code: {response?.StatusCode}");
                    return new List<T>();
                }
            }

        }

        public async Task<IEnumerable<T>> PostAllAsync<T>(IEnumerable<T> data, string url)
        {
            TokenInfo _token = await _tokenService.GetNewToken() ?? new TokenInfo();
       
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonSerializer.Serialize<IEnumerable<T>>(data), System.Text.Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(_configuration.ApiUrl!);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token!.access_token);
                var response = await client.PostAsync("", content);
                if (response is not null && response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<T>>(responseContent);

                    return result is null ? new List<T>() : result;
                }
                else
                {
                    _log.LogWarning($"Error when posting {typeof(T).Name}, http status code: {response?.StatusCode}");
                    return new List<T>();
                }
            }
        }

        public async Task<T?> PostAsync<T>(T? data, string url)
        {
            TokenInfo _token = await _tokenService.GetNewToken();
            if (data is null)
                data = default(T);
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonSerializer.Serialize<T>(data!), System.Text.Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(_configuration.ApiUrl!);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.access_token);
                var response = await client.PostAsync("", content);
                if (response is not null && response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<T>(responseContent);

                    return result;
                }
                else
                {
                    _log.LogWarning($"Error when getting {typeof(T).Name}, http status code: {response?.StatusCode}");
                    return default(T);
                }
            }
        }
    }

}