using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;

namespace AUTOAPI
{
    public class TokenService
    {
        private static TokenInfo? tokenInfo;
        private ExecutionContext _context;

        private readonly ILogger _log;

        public TokenService(ILogger logger, ExecutionContext context)
        {
            _log = logger;
            _context = context;
            tokenInfo = new TokenInfo();

        }

        public async Task<TokenInfo> GetHostCompanyTokenInfo()
        {

            if (tokenInfo is null)
                tokenInfo = new TokenInfo();

            if (HasTokenExpired(tokenInfo!))
                tokenInfo = await GetNewToken();
            
            return tokenInfo; 
        }

        public async Task<TokenInfo> GetNewToken()
        {
            string resourceName = "configHostCompany.json";

            var config = File.ReadAllText(Path.Combine(_context.FunctionAppDirectory, resourceName));

            if (String.IsNullOrEmpty(config))
                throw new IOException();
            ApiConfiguration apiConfig = JsonSerializer.Deserialize<ApiConfiguration>(config) ?? throw new IOException();

            ////To take Api token credential like username and password from Azure secret file
            var keyVaultUri = new Uri(@"https://Client.vault.azure.net/");//here you need to generate key vault url for client company
            var secretClient = new SecretClient(keyVaultUri, new DefaultAzureCredential());

            //string usernameSecretName = "UsernameSecret";
            //string passwordSecretName = "PasswordSecret";
            
            KeyVaultSecret usernameSecret = await secretClient.GetSecretAsync("HostCompanyUsername");// you need to set HostCompanyUsername in Azure KeyVaultSecret
            KeyVaultSecret passwordSecret = await secretClient.GetSecretAsync("HostCompanyPassword");// you need to set HostCompanyPassword in Azure KeyVaultSecret

            apiConfig!.Username = usernameSecret.Value;
            apiConfig.Password = passwordSecret.Value;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(apiConfig.AuthUrl!.Trim());
                httpClient.DefaultRequestHeaders.Accept.Clear();
                var authContent = $"client_id={apiConfig.ClientId}&grant_type=password&username={apiConfig.Username}&password={apiConfig.Password}";
                var content = new StringContent(authContent, Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage tokenResponse = await httpClient.PostAsync("Token", content);

                if (tokenResponse.IsSuccessStatusCode)
                {
                    string tokenResponseContent = await tokenResponse.Content.ReadAsStringAsync();
                    tokenInfo = JsonSerializer.Deserialize<TokenInfo>(tokenResponseContent) ?? throw new IOException();

                    tokenInfo.ExpirationTime = DateTime.UtcNow.AddSeconds(tokenInfo.expires_in);

                }
                else
                {
                    var errorContent = await tokenResponse.Content.ReadAsStringAsync();
                    _log.LogError($"Failed to call Token API. Status code: {tokenResponse.StatusCode}", errorContent);
                    throw new Exception($"Error: {errorContent}");
                }
            }
            return tokenInfo;
        } 
  
        bool HasTokenExpired(TokenInfo tokenInfo)
        {
            return DateTime.UtcNow > tokenInfo.ExpirationTime;
        }
    }
}