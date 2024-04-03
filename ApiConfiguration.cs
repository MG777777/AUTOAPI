using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace AUTOAPI
{
    public class ApiConfiguration
    {
        public string? AuthUrl { get; set; }
        public string? ApiUrl { get; set; }
        public string? PersonUrl { get; set; }
        public string? CompanyUrl { get; set; }
        public string? CostUnitsUrl { get; set; }
        public string? ClientId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }


       
    }
}
