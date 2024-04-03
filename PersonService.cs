using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.Threading;
using Microsoft.Azure.WebJobs;

namespace AUTOAPI
{
    public class PersonService
    {


        private readonly ILogger _log;
        private readonly Microsoft.Azure.WebJobs.ExecutionContext _context;

        public PersonService( ILogger log, Microsoft.Azure.WebJobs.ExecutionContext context)
        {

            this._log = log;
            this._context = context;
        }
        public async Task<IEnumerable<HostCompanyPerson>> RegisterNewPersonAsync(HostCompanyPerson person)
        {
            string resourceName = "configHostCompany.json";
            string jsonConfig = File.ReadAllText(Path.Combine(_context.FunctionAppDirectory, resourceName));

            if (String.IsNullOrEmpty(jsonConfig))
            {
                Console.WriteLine("configHostCompany.json is faulty");
                return new List<HostCompanyPerson>();
            }
            ApiConfiguration? apiConfig = JsonSerializer.Deserialize<ApiConfiguration>(jsonConfig) ?? throw new IOException();

            var HostCompanyApi = new HostCompanyApi(_log, _context);
            var persons = new List<HostCompanyPerson>();
            persons.Add(person);
            return await HostCompanyApi.PostAllAsync<HostCompanyPerson>(persons, apiConfig!.PersonUrl!);
        }
        public async Task<List<HostCompanyPerson>> GetAllPersons()
        {
            string resourceName = "configHostCompany.json";
            string jsonConfig = File.ReadAllText(Path.Combine(_context.FunctionAppDirectory, resourceName));

            if (String.IsNullOrEmpty(jsonConfig))
            {
                Console.WriteLine("configHostCompany.json is faulty");
                return new List<HostCompanyPerson>();
            }
            ApiConfiguration? apiConfig = JsonSerializer.Deserialize<ApiConfiguration>(jsonConfig);
            if (apiConfig == null)
                return new List<HostCompanyPerson>();
            var HostCompanyApi = new HostCompanyApi(_log, _context);
            var persons = await HostCompanyApi.GetAllAsync<HostCompanyPerson>(apiConfig!.PersonUrl!);
            return persons ?? new List<HostCompanyPerson>();
        }
    }
}

