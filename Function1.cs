using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;


namespace AUTOAPI
{
    public class Function1
    {

        private static TokenService? _tokenService;
        public HostCompanyApi? _HostCompanyApi;
        public ClientCompanyService? _ClientCompanyService;
        public CompanyService? _companyService;
        public PersonService? _personService;
        public CostunitService? _costunitService;



        [FunctionName("Function1")]
        //you can change runing automated time */30 * * * * 
        public async Task RunAsync([TimerTrigger("0 */30 * * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            log.LogInformation("Execution Context", context);
            try
            {
                if (_tokenService is null)
                    _tokenService = new TokenService(log, context);

                //Arrange ClientCompany API
                log.LogInformation("Fetching HostCompanyPersons from ClientCompany API");
                var ClientCompanyClient = new ClientCompanyService(log, context);
                var ClientcompanyPersons = await ClientCompanyClient.GetClientcompanyPersons();
                log.LogInformation($"Fetched {ClientcompanyPersons.Count} HostCompanyPersons from ClientCompany API");

                //Arrange HostCompany API
                var newTokenInfo = await _tokenService.GetHostCompanyTokenInfo();

                //Persons
                log.LogInformation("Fetching HostCompanyPersons from HostCompany API");
                PersonService personService = new PersonService(log, context);
                var HostCompanyPersons = await personService.GetAllPersons();
                log.LogInformation($"Fetched {HostCompanyPersons.Count} HostCompanyPersons from HostCompany API");

                //Companies
                log.LogInformation("Fetching Companies from HostCompany API");
                CompanyService companyService = new CompanyService(log, context);
                var companies = await companyService.GetAllCompanies();
                log.LogInformation($"Fetched {companies.Count} Companies from HostCompany API");

                //CostUnits
                log.LogInformation("Fetching CostUnits from HostCompany API");
                CostunitService costunitService = new CostunitService(log, context);
                List<HostCostUnit> costunits = await costunitService.GetAllCostUnits() ?? new List<HostCostUnit>();
                log.LogInformation($"Fetched {costunits.Count} CostUnits from HostCompany API");

                //Compare lists
                log.LogInformation("Comparing lists.");
                List<ClientcompanyPerson> personsToAdd = new List<ClientcompanyPerson>();
                foreach (var person in ClientcompanyPersons)
                {
                    //Employment and employee ID must exist 
                    if (person.job_information is null || String.IsNullOrEmpty(person.job_information?.employee_id))
                        continue;

                    if (!HostCompanyPersons.Any((p) => p.ID != null && p.ID.Equals(person.job_information?.employee_id)))
                    {
                        personsToAdd.Add(person);
                        //Console.WriteLine($"Could not match email {person.personal_data.work_email}, adding new user in HostCompany");
                    }
                }

                log.LogInformation($"Found {personsToAdd.Count} HostCompanyPersons to add to HostCompany");
                var personsAdded = new List<HostCompanyPerson>();
               foreach(var person in personsToAdd) 
                {
                  
                    var username = person.personal_data?.first_name?.ToLowerInvariant() + "." + person.personal_data?.last_name?.ToLowerInvariant();
                   
                    var team = person.job_information?.teams ?? new List<Team>();
                    var employerName = person.job_information?.employer;
                    HostCostUnit? costUnit = costunits!.Find((c) => 
                    { 
                        if (c.Description is not null) 
                            return c.Description == team!.FirstOrDefault()!.name; 
                        else return false; 
                    });

                    string costUnitID = costUnit?.ID ?? "";
                    string company = companies.Find((c) => c.Description == employerName?.name)?.ID ?? "";

                    HostCompanyPerson newPerson = new HostCompanyPerson()
                    {
                        ID = person.job_information?.employee_id,
                        Username = username,
                        FirstName = person.personal_data?.first_name,
                        LastName = person.personal_data?.last_name,
                        Company = company, //Match employer name with HostCompany  
                        CostUnitID = costUnitID, //Teams at ClientCompany
                        EmailPrimary = person.personal_data?.work_email,
                        EmployeeNo = person.job_information?.employee_id,
                        Enabled = false,
                        Overwrite = true
                    };
                    log.LogInformation($"Registered new employee with ID: {newPerson.ID}", newPerson);
                    personsAdded.Add(newPerson);
                    
                }
               
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}