using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AUTOAPI
{
    public class ClientCompanyService
    {
        private readonly ILogger _logger;
        private readonly ExecutionContext _context;
        public ClientCompanyService(ILogger logger, ExecutionContext context)
        {
            this._context = context;
            this._logger = logger;
        }
        public async Task<List<ClientcompanyPerson>> GetClientcompanyPersons()
        {
            try
            {
                string jsonConfig = File.ReadAllText(Path.Combine(_context.FunctionAppDirectory, "configClientCompany.json"));

                if (String.IsNullOrEmpty(jsonConfig))
                    throw new IOException();

                ApiConfigClient apiConfig = JsonSerializer.Deserialize<ApiConfigClient>(jsonConfig) ?? throw new IOException();

                //// To take Api token from Azure secret file
                var keyVaultUri = new Uri(@"https://ClientCompany.vault.azure.net/");//here you need to add url of key vault secret clientcompany
                var secretClient = new SecretClient(keyVaultUri, new DefaultAzureCredential());
              
                var apiTokenSecret = await secretClient.GetSecretAsync("ClientCompanyToken");
                apiConfig.ApiToken = apiTokenSecret.Value.Value.ToString() ?? "";

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiConfig.apiUrl!);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Subdomain", apiConfig.Subdomain);
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiConfig.ApiToken}");
                    var response = await client.GetFromJsonAsync<Root>(apiConfig.ApiUrlWithOffset());

                    List<ClientcompanyPerson> data = new List<ClientcompanyPerson>();
                    int limit = 0;
                    int total = 0;
                    if (response is not null && response.data is not null)
                    {
                        data = (response.data);

                        limit = response.meta!.limit;
                        apiConfig.offset++;
                        total = response.meta.total;

                        while (response.meta is not null && (limit * apiConfig.offset) <= total)
                        {
                            response = await client.GetFromJsonAsync<Root>(apiConfig.ApiUrlWithOffset());

                            if (response is not null && response.data is not null)
                            {
                                //add new data
                                data.AddRange(response.data);
                                //new meta
                                limit = response.meta!.limit;
                                apiConfig.offset++;
                                total = response.meta.total;
                            }
                            else
                                break;
                        }
                    }

                    if (data is not null && data.Count > 0)
                        return data;
                    else return new List<ClientcompanyPerson>();
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message, ex);
                return new List<ClientcompanyPerson>();
            }

        }
    }
}





