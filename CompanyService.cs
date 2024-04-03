using Microsoft.Extensions.Logging;
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
    public class CompanyService
    {

        private readonly ILogger _log;
        private readonly Microsoft.Azure.WebJobs.ExecutionContext _context;

        public CompanyService(ILogger log, Microsoft.Azure.WebJobs.ExecutionContext context)
        {
            this._log = log;
            this._context = context;
        }
      
        public async Task<List<HostCompany>> GetAllCompanies()
        {
            string resourceName = "configHostCompany.json";
            string jsonConfig = File.ReadAllText(Path.Combine(_context.FunctionAppDirectory, resourceName));
            if (String.IsNullOrEmpty(jsonConfig))
            {
                Console.WriteLine("configHostCompany.json is faulty");
                return new List<HostCompany>();
            }
            ApiConfiguration apiConfig = JsonSerializer.Deserialize<ApiConfiguration>(jsonConfig) ?? throw new IOException();
            if (apiConfig == null)
                return new List<HostCompany>();
            var HostCompanyApi = new HostCompanyApi(_log, _context);
            var companies = await HostCompanyApi.GetAllAsync<HostCompany>(apiConfig.CompanyUrl!);
            return companies ?? new List<HostCompany>();
        }
    }
}
