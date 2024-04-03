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
    public class ApiConfigClient
    {
        public string? ApiToken { get; set; }
        public string? Subdomain { get; set; }
        public string? apiUrl { get; set; }
        public int? offset { get; set; } = 0;

        public string ApiUrlWithOffset()
        {
            return apiUrl + $"&offset={100*offset}";
        }
    }
}
